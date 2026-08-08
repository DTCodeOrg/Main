using DataTransferModel;

namespace Main.Services;

public interface ITenancyService
{
    Task<TenantDisplayDataModel> FindHostAsync (string hostName);
}
