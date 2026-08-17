using Main.Common.Models;
using Main.IRepository;
using Main.Model.Identity;

namespace Main.Services.Services;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService (ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<TenantDataModel> FindHostAsync (string hostName)
    {
        Tenant? tenant = await _tenantRepository.FindHostAsync (hostName);

        if ( tenant == null )
        {
            throw new InvalidOperationException ("Tenant not found");
        }

        TenantDataModel tenantDataModel =
        new (tenant.TenantId, tenant.TenantName, tenant.Host, tenant.SecretKey );

        tenantDataModel.ResolvedTenantId = tenant.TenantId;

        return tenantDataModel;
    }
}
