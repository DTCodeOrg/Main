using Main.Infrastructure;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middlewares;

public class TenantSessionOptionsSetup: IConfigureOptions<SessionOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantSessionOptionsSetup (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Configure (SessionOptions options)
    {
        var context = _httpContextAccessor.HttpContext;
        var tenantSetter = context?.RequestServices.GetRequiredService<ITenantSetter>();

        if ( tenantSetter?.CurrentTenantId != null )
        {
            var tenantId = tenantSetter.CurrentTenantId.ToString ();

            options.Cookie.Name = $".Session.{tenantId}";
            options.Cookie.Domain = context!.Request.Host.Host;
            options.Cookie.Path = "/";
            options.Cookie.HttpOnly = true; // Protects temporary images in session from JS theft
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.IdleTimeout = TimeSpan.FromMinutes (30); // Clean session memory after 30 mins
        }
    }
}