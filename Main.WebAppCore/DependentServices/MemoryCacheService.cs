using Main.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.DependentServices;

public interface ITenantCacheService
{
    void Set<T> (string baseKey,T value,TimeSpan expiration);
    bool TryGet<T> (string baseKey,out T? value);
    void Clear (string baseKey);
}

public class TenantCacheService: ITenantCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantSetter  _tenantSetter;

    public TenantCacheService (ITenantSetter tenantSetter,IMemoryCache memoryCache,IHttpContextAccessor httpContextAccessor)
    {
        _memoryCache = memoryCache;
        _httpContextAccessor = httpContextAccessor;
        _tenantSetter = tenantSetter;
    }

    // Helper to extract the tenant and build the suffixed key
    private string BuildTenantKey (string baseKey)
    {
        var context = _httpContextAccessor.HttpContext;
        if ( context == null )
        {
            throw new InvalidOperationException ("HttpContext is not available.");
        }

        // Build the tenant-suffixed key: "myKey:tenant123"
        return $"{baseKey}:{_tenantSetter.ResolvedTenantId}";
    }

    public void Set<T> (string baseKey,T value,TimeSpan expiration)
    {
        string fullKey = BuildTenantKey(baseKey);

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        _ = _memoryCache.Set (fullKey,value,cacheOptions);
    }

    public bool TryGet<T> (string baseKey,out T? value)
    {
        string fullKey = BuildTenantKey(baseKey);
        return _memoryCache.TryGetValue (fullKey,out value);
    }

    public void Clear (string baseKey)
    {
        string fullKey = BuildTenantKey(baseKey);

        // Removes the item immediately from memory
        _memoryCache.Remove (fullKey);
    }
}
