using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Tenant;

namespace Main.Repository;

public class PanelRepository: IPanelRepository
{
    private readonly TenantDbContext _tenantContext;

    public PanelRepository ()
    {
    }


    public PanelRepository (TenantDbContext context)
    {
        _tenantContext = context;
    }

    public async Task<bool> DeletePanelAsync (int panelId)
    {
        Panel? panel = await _tenantContext.Panels.FindAsync ( panelId );

        if ( panel != null )
        {
            _ = _tenantContext.Panels.Remove (panel);
            int result = await _tenantContext.SaveChangesAsync();
            return result > 0;
        }

        return false;
    }
}

