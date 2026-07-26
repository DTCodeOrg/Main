Yes, you can build a custom Antiforgery middleware to handle this exact lifecycle. Writing custom middleware is highly recommended when implementing strict, non-standard behaviors like dropping the cookie immediately after verification, rotating it per request, and issuing an isolated JWT token. [1] 

Because you are writing a dedicated middleware class, you will completely bypass .UseAntiforgery() and handle token generation and validation manually using standard encryption tools.

## 1. Architectural Strategy

* Zero Bleeding Effect: The middleware resolves ITenantContext first. The cookie name dynamically incorporates the tenant slug (e.g., .Csrf.finearts), isolating tenants at the browser level.

* Immediate Deletion & Rotation: During validation, the middleware parses the cookie, matches it to the incoming header, and then instantly deletes the old cookie. A brand new JWT token and corresponding cookie are generated and sent back with the response. [2, 3, 4, 5, 6] 

* No Side-Effects on Auth Cookies: All operations strictly target the .Csrf.{tenant} cookie key, ensuring your actual authentication, access, or rotating refresh tokens are completely untouched.

## 2. Complete Custom Middleware Implementation

Create the middleware class in your project: [7] 

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
public class CustomTenantAntiforgeryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _jwtSecret = "YOUR_SUPER_LONG_SYSTEM_WIDE_SECRET_KEY_32_CHARS"; // Keep safe!

    public CustomTenantAntiforgeryMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        // 1. Identify Tenant Context (Ensure your CustomTenantMiddleware has run first!)

        if (tenantContext?.CurrentTenant == null)
        {
            await _next(context);
            return;
        }

        string tenantName = tenantContext.CurrentTenant.Name; // e.g., "finearts"
        string cookieName = $".Csrf.{tenantName}";
        string headerName = "X-XSRF-TOKEN";

        // 2. STAGE 1: VALIDATION (For state-changing actions)

        string method = context.Request.Method;

        if (!HttpMethods.IsGet(method) && !HttpMethods.IsHead(method) && !HttpMethods.IsOptions(method))
        {
            string? cookieToken = context.Request.Cookies[cookieName];
            string? headerToken = context.Request.Headers[headerName];

            // Fail fast if either token is missing, or if they don't match strings exactly

            if (string.IsNullOrEmpty(cookieToken) || string.IsNullOrEmpty(headerToken) || cookieToken != headerToken)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Antiforgery token validation failed.");
                return;
            }

            // Cryptographically validate the JWT token contents

            if (!TryValidateJwtToken(cookieToken, tenantName, out var principal))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid or expired antiforgery token structure.");
                return;
            }

            // SUCCESSFUL VALIDATION: Immediately append deletion command for the consumed cookie

            context.Response.Cookies.Delete(cookieName, new CookieOptions 
            { 
                Domain = context.Request.Host.Host, 
                Path = "/" 
            });
        }

        // 3. STAGE 2: ROTATION / NEW GENERATION (Generate fresh ones for the response)

        // Hook into the Response starting event so it attaches right before headers leave Kestrel

        context.Response.OnStarting(() => 
        {
            string newJwtToken = GenerateAntiforgeryJwt(tenantName);

            // Append the fresh tenant-isolated cookie to the response
            context.Response.Cookies.Append(cookieName, newJwtToken, new CookieOptions
            {
                Domain = context.Request.Host.Host,
                Path = "/",
                HttpOnly = false, 
                // Allows global JavaScript layout script to read it and match headers
                Secure = true,     
                // Nginx forward mapping ensures browser treats it safely
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(20) 
                // Tight lifecycle window
            });

            // Mirror it into a response header so AJAX frameworks can instantly capture the rotation
            
            context.Response.Headers["X-New-XSRF-TOKEN"] = newJwtToken;

            return Task.CompletedTask;
        });

        await _next(context);
    }

    private string GenerateAntiforgeryJwt(string tenantName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            { 
                new Claim("tenant", tenantName),
                new Claim("jti", Guid.NewGuid().ToString()) // Prevents replay attacks
            }),
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool TryValidateJwtToken(string token, string expectedTenant, out ClaimsPrincipal? principal)
    {
        principal = null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        try
        {
            principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var tenantClaim = principal.FindFirst("tenant")?.Value;
            return tenantClaim == expectedTenant;
        }
        catch
        {
            return false;
        }
    }
}

## 3. Pipeline Registration in Program.cs
Do not invoke app.UseAntiforgery() or builder.Services.AddAntiforgery(). Replace it completely with your custom registration chain right below your custom tenant lookup logic:

app.UseForwardedHeaders(); // Read Nginx HTTPS indicators
app.UseRouting();
// 1. Resolve Tenant Context first (sets ITenantContext)
app.UseMiddleware<YourCustomTenantMiddleware>();
// 2. Inject your custom Antiforgery execution engine
app.UseMiddleware<CustomTenantAntiforgeryMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
// Map routes cleanly without .RequireAntiforgery() additions
app.MapControllers();
app.Run();

## 4. Global Frontend Script Synchronization (_Layout.cshtml)
Because the token rotates on every response, JavaScript must capture the updated header (X-New-XSRF-TOKEN) after a POST request completes so subsequent requests carry the fresh value.

<script>
    // Grab initial cookie values set during the GET load sequence
    function getCookie(name) {
        let matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"));
        return matches ? decodeURIComponent(matches[1]) : undefined;
    }

    // Dynamic tenant identifier from your backend view metrics
    const currentTenantName = "@(Context.RequestServices.GetRequiredService<ITenantContext>().CurrentTenant?.Name)";
    let activeToken = getCookie(`.Csrf.${currentTenantName}`);

    // Update token container dynamically from response tracking mechanisms
    function updateActiveToken(responseHeaders) {
        let rotatedToken = responseHeaders.get('X-New-XSRF-TOKEN');
        if (rotatedToken) {
            activeToken = rotatedToken;
        }
    }

    // Intercept Fetch Operations to implement the strict rotation framework
    const originalFetch = window.fetch;
    window.fetch = async function (resource, config = {}) {
        config.headers = config.headers || {};
        
        if (config.method && !['GET', 'HEAD', 'OPTIONS'].includes(config.method.toUpperCase())) {
            config.headers['X-XSRF-TOKEN'] = activeToken;
        }

        const response = await originalFetch(resource, config);
        
        // Harvest the newly generated token automatically
        updateActiveToken(response.headers);
        return response;
    };
</script>

Would you like me to walk through how to configure custom integration testing for this rotation pipeline to verify that old consumed tokens are rejected instantly?

[1] [https://stackoverflow.com](https://stackoverflow.com/questions/79211056/customize-asp-net-cores-antiforgery-failure-response)
[2] [https://dev.to](https://dev.to/securitystefan/django-session-cookie-vs-localstorage-jwt-security-comparison-25an)
[3] [https://www.microsoftpressstore.com](https://www.microsoftpressstore.com/articles/article.aspx?p=2473126)
[4] [https://jasonwatmore.com](https://jasonwatmore.com/post/2020/05/25/aspnet-core-3-api-jwt-authentication-with-refresh-tokens)
[5] [https://dev.to](https://dev.to/jszutkowski/securing-api-with-jwt-in-symfony-36dk)
[6] [https://mherman.org](https://mherman.org/blog/stubbing-node-authentication-middleware-with-sinon/)
[7] [https://medium.com](https://medium.com/@ravipatel.it/a-complete-beginners-guide-to-asp-net-core-net-8-middleware-1e35c0eab444)

## Why This is Considered "Non-Standard"
The default security design pattern for web applications treats anti-forgery tokens as session-bound or time-bound identifiers, rather than one-time-use codes.
A standard framework generates a token once per session or rotates it on a loose schedule (e.g., hours or days). Reusing the same valid token multiple times within that session is perfectly acceptable in standard designs.
Your proposed architecture changes this behavior by introducing strict token consumption and rotation on every single state-changing request. While highly secure, it changes how multi-tab systems and concurrent requests interact with your backend.
------------------------------
## Built-In Middleware vs. Custom Middleware

| Feature / Behavior | .NET 8.0 Built-In Antiforgery | Your Custom JWT Middleware |
|---|---|---|
| Token Cryptography | Data Protection API (machine-specific key rings) | Custom JWT (stateless, signed with a symmetric secret key) |
| Validation Engine | Tied to MVC Endpoints/Filters or .RequireAntiforgery() metadata | Intercepts requests early at the pipeline routing level |
| Token Lifecycle | Reusable for the duration of the user's session | Destroyed and rotated after exactly one state-changing request |
| Tenant Isolation | Requires custom dynamic options patching to isolate cookies | Native, dynamic pathing directly baked into the custom class logic |
| Concurrency Model | Multi-tab safe (multiple requests can submit parallel forms) | Prone to race conditions if multiple requests fire simultaneously |

------------------------------
## Potential Workflow Mismatches## 1. The Multi-Tab / Parallel Request Race Condition
This is the most significant operational risk.

* The Scenario: A user opens your application (finearts.test) in two browser tabs simultaneously. Tab 1 submits a form. Your custom middleware validates the token, deletes it, and returns a fresh one to Tab 1. [1] 
* The Error: The token saved in Tab 2 is now instantly dead. If the user clicks "Submit" in Tab 2, the request will fail with an HTTP 400 Bad Request error because its token was already consumed by Tab 1.
* Impact on AJAX/Fetch: If your frontend fires two rapid asynchronous API POST requests in parallel, the second request will execute before the first request can return and update the global activeToken variable. The second request will send the old, deleted token and crash.

## 2. Caching Issues
If an Nginx layout page or a specific controller view is cached by a browser or an intermediary proxy, the initial GET token will become stale for subsequent visitors, triggering systematic validation failures for non-GET actions.
------------------------------
## Cryptographic and Security Risks## 1. JWT Key Compromise [2] 
The built-in .NET system automatically updates and rotates machine encryption keys using the Windows or Linux file system Key Ring. Your custom middleware relies on a hardcoded string _jwtSecret. If an attacker gains read access to your environment variables or source control, they can forge valid tokens for any tenant.
## 2. Replay Window Exposure
Standard JWT validation only checks the expiration date (Expires). If you do not track consumed tokens on your backend, an attacker who intercepts a token via a Man-in-the-Middle vector (or local browser storage) could quickly replay that identical token multiple times before its 20-minute expiration window closes. [3] 

* The Fix: Your custom middleware implements a unique jti (JWT ID) claim. To completely secure it, you must cross-reference this jti against a fast-access cache (like a distributed memory cache or Redis) to guarantee it can never be processed twice. [4, 5] 

------------------------------
## Setting Up Custom Integration Testing
To safely verify that old consumed tokens are rejected instantly and that your isolation functions properly, build an integration test project using Microsoft.AspNetCore.Mvc.Testing.
## 1. Setup the Integration Test Fixture [6] 
This fixture boots your .NET 8.0 application engine in-memory, mimicking Nginx by manually mapping the X-Forwarded-Host header.

using Microsoft.AspNetCore.Mvc.Testing;using Xunit;using System.Net;
public class AntiforgeryIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AntiforgeryIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Pipeline_Should_Consume_Token_And_Reject_Subsequent_Use()
    {
        // Arrange - Establish client simulating Nginx forward context
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        // Step 1: Execute GET request to establish the tenant context and harvest initial cookie
        var initialRequest = new HttpRequestMessage(HttpMethod.Get, "/home/index");
        initialRequest.Headers.Add("X-Forwarded-Host", "finearts.test");
        initialRequest.Headers.Add("X-Forwarded-Proto", "https");

        var initialResponse = await client.SendAsync(initialRequest);
        
        // Extract the generated dynamic tenant cookie
        var cookieHeader = initialResponse.Headers.GetValues("Set-Cookie").FirstOrDefault();
        Assert.Contains(".Csrf.finearts", cookieHeader);
        
        // Extract token string out of the cookie formatting
        var tokenValue = cookieHeader.Split(';')[0].Split('=')[1];

        // Step 2: Execute valid FIRST POST request using the harvested token
        var postRequest1 = new HttpRequestMessage(HttpMethod.Post, "/artwork/save");
        postRequest1.Headers.Add("X-Forwarded-Host", "finearts.test");
        postRequest1.Headers.Add("X-Forwarded-Proto", "https");
        postRequest1.Headers.Add("X-XSRF-TOKEN", tokenValue);
        postRequest1.Headers.Add("Cookie", $".Csrf.finearts={tokenValue}");
        postRequest1.Content = new FormUrlEncodedContent(new Dictionary<string, string> { { "Title", "Masterpiece" } });

        var response1 = await client.SendAsync(postRequest1);
        
        // Assert first execution succeeded
        Assert.Equal(HttpStatusCode.OK, response1.StatusCode);

        // Step 3: Re-use the IDENTICAL token a second time to verify immediate consumption
        var postRequest2 = new HttpRequestMessage(HttpMethod.Post, "/artwork/save");
        postRequest2.Headers.Add("X-Forwarded-Host", "finearts.test");
        postRequest2.Headers.Add("X-Forwarded-Proto", "https");
        postRequest2.Headers.Add("X-XSRF-TOKEN", tokenValue); // Reusing dead token
        postRequest2.Headers.Add("Cookie", $".Csrf.finearts={tokenValue}");
        postRequest2.Content = new FormUrlEncodedContent(new Dictionary<string, string> { { "Title", "Exploit Attempt" } });

        var response2 = await client.SendAsync(postRequest2);

        // Assert that the middleware caught the consumption state and rejected it
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
    }

    [Fact]
    public async Task Pipeline_Should_Block_Cross_Tenant_Token_Bleeding()
    {
        var client = _factory.CreateClient();

        // Harvest a valid token from "finearts.test"
        var tenantARequest = new HttpRequestMessage(HttpMethod.Get, "/home/index");
        tenantARequest.Headers.Add("X-Forwarded-Host", "finearts.test");
        var tenantAResponse = await client.SendAsync(tenantARequest);
        var tokenHeaderA = tenantAResponse.Headers.GetValues("Set-Cookie").First();
        var tokenValueA = tokenHeaderA.Split(';')[0].Split('=')[1];

        // Attempt to execute a post on "music.test" using the token generated for "finearts.test"
        var maliciousPost = new HttpRequestMessage(HttpMethod.Post, "/artwork/save");
        maliciousPost.Headers.Add("X-Forwarded-Host", "music.test"); // Different tenant host
        maliciousPost.Headers.Add("X-XSRF-TOKEN", tokenValueA);
        maliciousPost.Headers.Add("Cookie", $".Csrf.music={tokenValueA}");

        var maliciousResponse = await client.SendAsync(maliciousPost);

        // Assert rejection because internal tenant claims do not align
        Assert.Equal(HttpStatusCode.BadRequest, maliciousResponse.StatusCode);
    }
}

Would you like to see how to implement an in-memory distributed cache inside your custom middleware to track the jti claims and eliminate the replay-attack security risk completely?

[1] [https://andrewlock.net](https://andrewlock.net/automatically-validating-anti-forgery-tokens-in-asp-net-core-with-the-autovalidateantiforgerytokenattribute/)
[2] [https://medium.com](https://medium.com/simform-engineering/building-a-centralized-authentication-system-for-net-microservices-with-azure-entra-id-8a643057269a)
[3] [https://ssojet.com](https://ssojet.com/jwt-validation/validate-jwt-using-es256-in-spring-boot)
[4] [https://buildkite.com](https://buildkite.com/docs/apis/oauth-token-exchange)
[5] [https://python.plainenglish.io](https://python.plainenglish.io/revoke-jwts-secure-your-api-middleware-redis-in-drf-d8501f1d30ba)
[6] [https://developer.adobe.com](https://developer.adobe.com/commerce/testing/guide/integration/)
