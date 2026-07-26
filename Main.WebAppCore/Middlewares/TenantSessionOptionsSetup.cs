using Main.Infrastructure;
using Microsoft.Extensions.Options;
namespace Main.WebAppCore.Middleware;

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
            // Lock session identity explicitly to the isolated tenant domain
            options.Cookie.Name = $".Session.{tenantSetter.CurrentTenantId}";
            options.Cookie.Domain = context.Request.Host.Host;
            options.Cookie.Path = "/";
        }
    }
}