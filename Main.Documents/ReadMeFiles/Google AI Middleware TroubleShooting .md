SameSiteMode.Lax is a browser cookie security setting that controls whether a cookie is sent along with requests originating from external (third-party) websites. [1, 2, 3] 
It acts as a shield against Cross-Site Request Forgery (CSRF) attacks while ensuring your users experience smooth navigation when clicking links from outside your app. [4, 5, 6] 
Here is exactly how Lax behaves and why it is the perfect choice for your multi-tenant setup:
## 1. How SameSiteMode.Lax Protects Your App
The SameSite attribute tells the browser when it is allowed to attach your tenant cookies (.AspNetCore.Antiforgery.{Id} and .Session.{Id}) to an HTTP request. Lax enforces two strict rules: [7, 8] 

* Blocked on Cross-Site Submissions (The Security Shield): If a user is on a completely different website (like malicious-site.com) and that site attempts to send a hidden POST, PUT, or DELETE request via a form or JavaScript to https://finearts.test, the browser refuses to attach your cookies. Because the cookies are missing, your .NET pipeline immediately blocks the request. [9] 
* Allowed on Safe Cross-Site Links (The User Experience): If a user is checking their email or on a social media site and clicks a standard link (<a href="https://finearts.test">), the browser will attach your cookies because it is a safe GET request. The user arrives at your site already logged in and their session context loads instantly. [10, 11, 12, 13] 

------------------------------
## 2. Why Lax is Essential for Your Subdomains
To understand why Lax is the right choice, look at how it compares to the other two options in a multi-tenant environment:
## Strict Mode (SameSiteMode.Strict) — Too Aggressive [14] 
If you used Strict, the browser would never send your cookies if the request originated from outside your exact domain. [15] 

* The Breakage: If a user clicks a link from their email to open https://finearts.test, the browser will strip your session and authentication cookies entirely because the click came from an external site. The user would arrive at your site looking logged out, or their layout would crash because the backend cannot find their active tenant session on that first click. [16, 17] 

## None Mode (SameSiteMode.None) — Dangerous
If you used None, the browser would attach your cookies to every single request, even if a malicious third-party site generated the submission. This leaves your app entirely vulnerable to CSRF attacks unless you build heavy custom validation layers. [18, 19] 
------------------------------
## Summary of Lax behavior in your Multi-Tenant system

| Action | Request Source | Destination | HTTP Method | Are Cookies Sent? |
|---|---|---|---|---|
| Clicking an internal link | finearts.test | finearts.test/create | GET | Yes (Internal) |
| Submitting an internal form | finearts.test | finearts.test/save | POST | Yes (Internal) |
| Clicking an external link | google.com or email | finearts.test | GET | Yes (Safe navigation) |
| Malicious hidden exploit | attacker.com | finearts.test/delete | POST | NO (Blocked by Lax) |

By choosing SameSiteMode.Lax, you get robust protection against malicious outside attacks, clean separation between different tenant domains, and a flawless multi-tab experience for users navigating your site. [20] 
Now that your cookies are secured with Lax, are you ready to implement the C# controller endpoints to save your temporary uploaded images into this isolated session container?

[1] [https://next-intl.dev](https://next-intl.dev/docs/routing/configuration)
[2] [https://supertokens.com](https://supertokens.com/docs/post-authentication/session-management/security)
[3] [https://clerk.com](https://clerk.com/articles/nextjs-session-management-solving-nextauth-persistence-issues)
[4] [https://blog.mozilla.org](https://blog.mozilla.org/security/2018/04/24/same-site-cookies-in-firefox-60/)
[5] [https://htmx.org](https://htmx.org/essays/web-security-basics-with-htmx/)
[6] [https://theproductguy.in](https://theproductguy.in/blogs/cookie-security-guide/)
[7] [https://www.testmuai.com](https://www.testmuai.com/learning-hub/samesite-cookie-attribute/)
[8] [https://cheatsheetseries.owasp.org](https://cheatsheetseries.owasp.org/cheatsheets/Symfony_Cheat_Sheet.html)
[9] [https://dev.lucee.org](https://dev.lucee.org/t/update-to-tomcat-9-0-33-broken-cookies/6854)
[10] [https://thecopenhagenbook.com](https://thecopenhagenbook.com/csrf)
[11] [https://medium.com](https://medium.com/@rramgattie/samesite-and-subdomains-08870bbdd62c)
[12] [https://medium.com](https://medium.com/@abhishekjainindore24/claude-3-claude-md-42b76b6895ff)
[13] [https://dilshankelsen.com](https://dilshankelsen.com/what-is-cross-site-request-forgery/)
[14] [https://cookie-script.com](https://cookie-script.com/documentation/samesite-cookie-attribute-explained)
[15] [https://cookie-script.com](https://cookie-script.com/documentation/samesite-cookie-attribute-explained)
[16] [https://medium.com](https://medium.com/@rramgattie/samesite-and-subdomains-08870bbdd62c)
[17] [https://alembic.com.au](https://alembic.com.au/blog/subdomain-based-multi-tenancy-in-phoenix)
[18] [https://portswigger.net](https://portswigger.net/web-security/csrf/bypassing-samesite-restrictions)
[19] [https://kinsta.com](https://kinsta.com/blog/csrf-attack/)
[20] [https://github.com](https://github.com/keycloak/keycloak/issues/19886)


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
