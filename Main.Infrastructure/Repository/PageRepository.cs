using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;
using Main.Model.Tenant;
using Microsoft.EntityFrameworkCore;
namespace Main.Repository;

public class PageRepository: IPageRepository
{

    private readonly TenantDbContext _tenantContext;

    public PageRepository (TenantDbContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public async Task<List<Page>> GetAllPages ()
    {
        List<Page> listPages = await _tenantContext.Pages.ToListAsync();

        return listPages.ToList ();
    }

    public async Task<Page> GetSinglePage (int id)
    {
        var page = await _tenantContext.Pages.FirstOrDefaultAsync<Page> (m => m.PageID == id);

        if ( page == null )
        {
            return new Page ();
        }

        return page;
    }

    public async Task<bool> UpdatePage (Panel panel,List<Post> listPosts)
    {
        panel.ListPosts = listPosts;

        Page? page = await _tenantContext.Pages.FirstOrDefaultAsync<Page>
                                  ( m => m.PageID == panel.PageID );

        if ( page == null )
        {
            return false;
        }

        page.CreatePanel (panel);

        _ = _tenantContext.Pages.Update (page);

        int result = await _tenantContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdatePage (Page page,List<Panel> listPanels)
    {
        page.ListPanels = listPanels;

        _ = _tenantContext.Pages.Update (page);

        int result = await _tenantContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> PageExists (int id)
    {
        return await _tenantContext.Pages.AnyAsync (e => e.PageID == id);
    }
}

