using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Main.WebAppCore.ActionFilters;

public class TenantAntiforgeryFilter: IAsyncActionFilter
{
    private readonly IAntiforgery _antiforgery;
    private const string TenantHeaderKey = "X-Tenant-ID";
    private const string BaseCookieName = ".AspNetCore.Antiforgery";

    public TenantAntiforgeryFilter (IAntiforgery antiforgery)
    {
        _antiforgery = antiforgery ?? throw new ArgumentNullException (nameof (antiforgery));
    }

    public async Task OnActionExecutionAsync (ActionExecutingContext context,ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        // 1. Fetch the tenant cleanly from your DI service (hydrated by your resolver middleware)
        string activeTenantId = _tenantSetter.CurrentTenantId.ToString();
        string tenantCookieName = $"{BaseCookieName}.{activeTenantId}";

        // 2. GET Requests: Generate token and append the suffixed cookie
        if ( HttpMethods.IsGet (httpContext.Request.Method) )
        {
            var tokens = _antiforgery.GetTokenSet(httpContext);

            httpContext.Response.Cookies.Append (tenantCookieName,tokens.CookieToken!,new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/" // Always keep path global; isolation happens via name suffix
            });

            _ = await next ();
            return;
        }

        // 3. POST/PUT Requests: Extract and swap the parsed feature collection
        var cookiesFeature = httpContext.Features.Get<IRequestCookiesFeature>();
        var originalCookiesCollection = cookiesFeature?.Cookies ?? httpContext.Request.Cookies;

        _ = originalCookiesCollection.TryGetValue (tenantCookieName,out var tenantCookieValue);

        try
        {
            if ( !string.IsNullOrEmpty (tenantCookieValue) )
            {
                var tempCookies = originalCookiesCollection.ToDictionary(k => k.Key, k => k.Value);

                // Swap the tenant value into the default configuration position
                tempCookies[BaseCookieName] = tenantCookieValue;

                var customCookiesFeature = new RequestCookiesFeature(httpContext.Features)
                {
                    Cookies = new RequestCookieCollection(tempCookies)
                };
                httpContext.Features.Set<IRequestCookiesFeature> (customCookiesFeature);
            }

            // Validate natively using the swapped layout
            await _antiforgery.ValidateRequestAsync (httpContext);
        }
        catch ( AntiforgeryValidationException )
        {
            context.Result = new BadRequestObjectResult (new
            {
                Error = "Security validation failed."
            });
            return;
        }
        finally
        {
            // 4. Restore state back to original
            var restoreFeature = new RequestCookiesFeature(httpContext.Features)
            {
                Cookies = originalCookiesCollection
            };
            httpContext.Features.Set<IRequestCookiesFeature> (restoreFeature);
        }

        _ = await next ();
    }

}
