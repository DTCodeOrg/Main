using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middlewares;

public class TenantAntiforgeryOptionMiddleware: IConfigureOptions<AntiforgeryOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantAntiforgeryOptionMiddleware (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Configure (AntiforgeryOptions options)
    {
        var context = _httpContextAccessor.HttpContext;
        var tenantSetter = context?.RequestServices.GetRequiredService<ITenantSetter>();

        if ( tenantSetter?.ResolvedTenantId != null )
        {
            var tenantId = tenantSetter.ResolvedTenantId.ToString(); // e.g., "finearts"

            options.Cookie.Name = $".AspNetCore.Antiforgery.{tenantId}";
            options.Cookie.Domain = context!.Request.Host.Host; // Locked to "finearts.test"
            options.Cookie.Path = "/";
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax; // Multi-tab and cross-tab navigation safe

        }
    }
}
