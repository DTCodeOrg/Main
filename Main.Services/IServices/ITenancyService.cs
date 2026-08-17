using Main.Common.Models;

namespace Main.Services;

public interface ITenancyService
{
    Task<TenantDataModel> FindHostAsync (string hostName);
}
