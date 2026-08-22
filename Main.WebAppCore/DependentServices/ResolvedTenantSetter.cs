using Main.Common.Models;
using Main.Infrastructure;
using System.Security.Claims;

namespace Main.WebAppCore.DependentServices;

public class ResolvedTenantSetter: ITenantSetter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResolvedTenantSetter (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        CurrentTenant = new TenantDataModel ();
    }

    public TenantDataModel CurrentTenant
    {
        get; set;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string HttpContextUserId => User?.FindFirst (ClaimTypes.NameIdentifier)?.Value is string userId ? userId : string.Empty;

    public string HttpContextTenantRole
    {
        get
        {
            return User?.FindFirst ("TenantRole")?.Value ?? "";
        }
    }

    public Guid HttpContextTenantId
    {
        get => _httpContextAccessor.HttpContext?.Items["TenantId"] is Guid id ? id : Guid.Empty;
    }

    public DateTime GetLocalNow ()
    {
        string timeZoneId = "Bangladesh Standard Time";
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc (DateTime.UtcNow,userTimeZone);
    }

    public BaseDataModel CreateMetaData
    {
        get
        {
            BaseDataModel baseDataModel = new ()
            {
                CreatedDate = GetLocalNow ( ),
                CreatedBy = HttpContextUserId,
                TenantUserId = HttpContextUserId,
                TenantCountry = AppSettings.Current.EnumCountry,
                IsActive = true
            };

            return baseDataModel;
        }
    }

    public BaseDataModel UpdateMetaData
    {
        get
        {
            BaseDataModel baseDataModel = new ()
            {
                ModifiedDate = GetLocalNow ( ) ,
                ModifiedBy = HttpContextUserId.ToString(),
                IsActive = true,
                TenantCountry = AppSettings.Current.EnumCountry
            };

            return baseDataModel;
        }
    }

    public BaseDataModel DeleteMetaData
    {
        get
        {
            BaseDataModel baseDataModel = new ()
            {
                DeletedDate = GetLocalNow ( ),
                DeletedBy = HttpContextUserId.ToString(),
                TenantCountry = AppSettings.Current.EnumCountry,
                IsActive = true,
            };

            return baseDataModel;
        }
    }

    public Guid ResolvedTenantId
    {
        get;
        set;
    }
}