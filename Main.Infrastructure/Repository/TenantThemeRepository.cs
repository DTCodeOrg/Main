using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class ThemeRepository: IThemeRepository
{
    private readonly IdentityAppDbContext _context;

    public ThemeRepository (IdentityAppDbContext context)
    {
        _context = context;
    }

    public async Task<TenantTheme> GetThemeByTenantAsync (Guid tenantId)
    {
        var tenantTheme =
        await _context.TenantThemes.FirstOrDefaultAsync (t => t.TenantId == tenantId);

        if ( tenantTheme == null )
        {
            return new TenantTheme ()
            {
                PrimaryColor = "#000000",
                SecondaryColor = "#FFFFFF",
                BackgroundColor = "#F0F0F0",
                FontStack = "Arial, sans-serif"
            };
        }

        return tenantTheme;
    }

    public async Task<TenantTheme> GetTenantThemeAsync (Guid tenantId)
    {
        var tenantTheme = await _context.TenantThemes.FirstOrDefaultAsync
        (t => t.TenantId == tenantId);

        if ( tenantTheme == null )
        {
            tenantTheme = new TenantTheme
            {
                Id = Guid.NewGuid (),
                TenantId = tenantId,
                PrimaryColor = "#000000",
                SecondaryColor = "#FFFFFF",
                BackgroundColor = "#F0F0F0",
                FontStack = "Arial, sans-serif",
                LogoFilePath = null
            };

            _ = _context.TenantThemes.Add (tenantTheme);

            _ = await _context.SaveChangesAsync ();
        }

        return tenantTheme;
    }

    public async Task UpdateTenantThemeAsync (TenantTheme theme)
    {
        _ = _context.TenantThemes.Update (theme);

        _ = await _context.SaveChangesAsync ();
    }
}
