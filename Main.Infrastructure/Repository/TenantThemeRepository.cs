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

    public async Task<TenantTheme> GetTenantThemeAsync (Guid themeId)
    {
        var TenantTheme = await _context.TenantThemes.FirstOrDefaultAsync (t => t.Id == themeId);

        if ( TenantTheme == null )
        {
            TenantTheme = new TenantTheme
            {
                Id = Guid.NewGuid (),
                PrimaryColor = "#000000",
                SecondaryColor = "#FFFFFF",
                BackgroundColor = "#F0F0F0",
                FontStack = "Arial, sans-serif",
                LogoFileName = null
            };

            _ = _context.TenantThemes.Add (TenantTheme);

            _ = await _context.SaveChangesAsync ();
        }

        return TenantTheme;
    }

    public async Task UpdateTenantThemeAsync (TenantTheme theme)
    {
        _ = _context.TenantThemes.Update (theme);

        _ = await _context.SaveChangesAsync ();
    }

}
