namespace Main.WebAppCore.Middlewares;

using Main.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class TenantSessionCookieMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<SessionOptions> _globalSessionOptions;

    public TenantSessionCookieMiddleware (RequestDelegate next,IOptions<SessionOptions> globalSessionOptions)
    {
        _next = next;
        _globalSessionOptions = globalSessionOptions;
    }

    public async Task InvokeAsync (HttpContext context,ITenantSetter tenantSetter)
    {
        if ( tenantSetter?.CurrentTenantId != null )
        {
            _globalSessionOptions.Value.Cookie.Domain = context!.Request.Host.Host;
            _globalSessionOptions.Value.Cookie.Path = "/";
            _globalSessionOptions.Value.Cookie.HttpOnly = true; // Protects temporary images in session from JS theft
            _globalSessionOptions.Value.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            _globalSessionOptions.Value.Cookie.SameSite = SameSiteMode.Lax;
            _globalSessionOptions.Value.IdleTimeout = TimeSpan.FromMinutes (30); // Clean session memory after 30 mins
            var tenantId = tenantSetter.CurrentTenantId.ToString();

            // Override the default cookie name seamlessly for this request context only
            // without altering the global application-wide Singleton options state.
            _globalSessionOptions.Value.Cookie.Name = $".Session.{tenantId}";
        }

        await _next (context);
    }
}
