using DataTransferModel;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.DependentServices;

public static class TenantResolutionExtensions
{
    public static async Task<TenantDisplayDataModel?> TryResolveTenantAsync (
        this HttpContext context,
        ITenancyService tenancyService,
        IMemoryCache memoryCache)
    {

        string host = context.Request.Host.Host; // "finearts.test"   

        string? tenantHost = context.ResolveFromDomain(host);

        if ( !string.IsNullOrEmpty (tenantHost) )
        {
            TenantDisplayDataModel? tenantDisplayDataModel =
            await memoryCache.GetOrCreateAsync($"tenant_{tenantHost}", async entry =>
            {
                _ =  entry.SetSize(1) ;
                _ =  entry.SetSlidingExpiration(TimeSpan.FromHours(1)) ;
                _ =  entry.SetAbsoluteExpiration(TimeSpan.FromHours(1)) ;

                return await tenancyService.FindHostAsync (tenantHost);
            });

            if ( tenantDisplayDataModel != null )
            {
                return tenantDisplayDataModel;
            }
        }

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
        if ( segments != null && segments.Length > 0 )
        {
            return segments[0];
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