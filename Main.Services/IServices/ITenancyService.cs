using Main.Common;

namespace Main.Services;

public interface ITenancyService
{
    Task<TenantDataModel> FindHostAsync (string hostName);
}
