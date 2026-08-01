using DataTransferModel;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public static class TenantResolutionExtensions
{
    public static async Task<TenantDisplayDataModel?> TryResolveTenantAsync (
        this HttpContext context,
        ITenantContext tenantContext,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IMemoryCache memoryCache,
        string rootDomain,
        ILogger<ExceptionLoggingService> logger)
    {
        // 1. Grab the host string directly from the browser's incoming request headers
        string host = context.Request.Host.Host; // e.g., "finearts.test"   

        logger.LogWarning (context.Request.Host.Host.ToString ());
        logger.LogWarning (host.ToString ());
        string? tenantHost = //context.ResolveFromSubdomain(rawHost)
                           // ?? 
                            context.ResolveFromDomain(host);
        //ReutePathExtensions.ResolveFromPath(context, tenantPath);
        /// logger.LogWarning (tenantHost + "Not fonud");
        if ( !string.IsNullOrEmpty (tenantHost) )
        {
            TenantDisplayDataModel? tenantDisplayDataModel =
            await memoryCache.GetOrCreateAsync($"tenant_{tenantHost}", async entry =>
            {
                // 1. MANDATORY: Set the size to satisfy your global SizeLimit
                _ =  entry.SetSize(1) ;

                // Resets the 1-hour lifetime every time this tenant is requested
                _ =  entry.SetSlidingExpiration(TimeSpan.FromHours(1)) ;

                // 2. OPTIONAL: Set how long this tenant data stays in memory
                _ =  entry.SetAbsoluteExpiration(TimeSpan.FromHours(1)) ;

                return await tenancyService.FindHostAsync (tenantHost);
            });

            logger.LogWarning (tenantDisplayDataModel != null ? tenantDisplayDataModel.MyTenantId.ToString () : "Id Not Found");

            if ( tenantDisplayDataModel != null )
            {

                logger.LogWarning (tenantSetter.CurrentTenantId.ToString ());
                return tenantDisplayDataModel;
            }
        }

        //context.Response.Redirect (rootDomain);
        return null;
    }



    private static string ResolveFromSubdomain (this HttpContext context,string host)
    {

        string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

        segments = RemoveResevedWord (segments.Length > 0 ? segments : null);

        if ( segments!.Length > 2 )
        {
            return segments[0];
        }

        return "";
    }

    private static string? ResolveFromDomain (this HttpContext context,string host)
    {
        string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

        //segments = RemoveResevedWord (segments!.Length > 0 ? segments! : null);

        // FIX: Change '> 1' to '> 0' because your tenant key might be the only segment left
        if ( segments != null && segments.Length > 0 )
        {
            return segments[0]; // This will now successfully return "finearts"
        }

        return "";

    }

    private static string[]? RemoveResevedWord (string[]? segments)
    {
        if ( segments!.Length > 0 && segments[0] == "www" )
        {
            segments = segments.Skip (1).ToArray ();
        }

        return segments;
    }

}