using DataTransferModel;
using Main.Common;
using Main.Common.Models;
using Main.IRepository;
using Main.Model.Identity;
using Main.Model.Tenant;
using Main.Services.Extensions;
namespace Main.Services;

public class PageService: IPageService
{
    public readonly IProductRepository _productRepository;
    public readonly IAdminPostRepository _adminPostRepository;
    public readonly IPageRepository _pageRepository;
    public readonly IPanelRepository _panelRepository;
    public readonly IPagePanelSettingsRepository _pagePanelSettingsRepository;

    public PageService (
        IProductRepository productRepository,
        IAdminPostRepository adminPostRepository,
        IPageRepository pageRepository,
        IPanelRepository panelRepository,
        IPagePanelSettingsRepository pagePanelSettingsRepository)
    {
        _productRepository = productRepository;
        _pageRepository = pageRepository;
        _adminPostRepository = adminPostRepository;
        _panelRepository = panelRepository;
        _pagePanelSettingsRepository = pagePanelSettingsRepository;
    }

    public async Task<bool> CreateNewPanel (PanelDataModel pagePanelDataModel)
    {
        Panel panelEntity = PageServiceMapping.CreatePanelEntity ( pagePanelDataModel );

        List<Post> listPostEntity = PageServiceMapping.CreateListPostEntity ( pagePanelDataModel );

        var result = await _pageRepository.UpdatePage ( panelEntity, listPostEntity );

        return result;
    }

    public async Task<List<PostDataModel>> GetSelectProducts ()
    {
        List<Product> listProducts
        = await _pagePanelSettingsRepository.GetSelectProducts ();

        List<PostDataModel> listPanelPostDataModel = PageServiceMapping.GetPostDataModels( listProducts );

        return listPanelPostDataModel;
    }

    public async Task<List<PostDataModel>> GetSelectPosts ()
    {
        List<AdminPost> listAdminPosts = await _adminPostRepository.GetSelectAdminPosts(  );

        List<PostDataModel> listPanelPostDataModel
         = PageServiceMapping.GetPostDataModels   ( listAdminPosts );

        return listPanelPostDataModel;
    }

    public async Task<PageDataModel> GetPageDataModel (int pageID)
    {
        Page pageEntity =  await _pageRepository.GetSinglePage ( pageID );
        PageDataModel pageDataModel =  PageServiceMapping.MapPageDataModel(pageEntity);
        return pageDataModel;
    }

    public async Task<PageDataModel> GetPageDataModel (EnumPublicPage page)
    {
        Page pageEntity =  await _pageRepository.GetSinglePage ( page );
        PageDataModel pageDataModel = PageServiceMapping.MapPageDataModel ( pageEntity );
        return pageDataModel;
    }

    public async Task<List<PageDisplayDataModel>> GetAllPages (string company)
    {
        List<Page> listPageEntity = await _pageRepository.GetAllPages ( );
        List<PageDisplayDataModel> listPageDisplayDataModel = [];

        listPageEntity.ForEach (pageEntity =>
        {
            listPageDisplayDataModel.Add (new PageDisplayDataModel
                        (pageEntity.PageID,
                        pageEntity.EnumPublicPage,
                        company));

        });

        return listPageDisplayDataModel.ToList ();
    }

    public async Task<bool> UpdatePanelsOrderAsync
    (List<PanelPositionDataModel> listPanelPositionDataModel,BaseDataModel baseDataModel,int pageId)
    {
        ArgumentNullException.ThrowIfNull (listPanelPositionDataModel);

        List<int> listPanelIds =
        listPanelPositionDataModel.Select(x => x.PanelID).ToList();

        Page page = await _pageRepository.GetSinglePage ( pageId );

        List<Panel> listPanels = page.ListPanels.ToList<Panel> () ?? [];

        listPanels.Where (panel =>
        {
            return listPanelIds.Contains (panel.PanelID);

        }).ToList ().ForEach (updatePanel =>
        {
            updatePanel.ModifyParameters (baseDataModel);

            updatePanel.PanelPosition =
            listPanelPositionDataModel.First (a => a.PanelID == updatePanel.PanelID).PanelPosition;

        });

        page.ModifyParameters (baseDataModel);

        bool result = await _pageRepository.UpdatePage ( page, listPanels );

        return result;
    }

    public async Task<bool> DeletePanelAsync (int panelId)
    {
        bool result = await _panelRepository.DeletePanelAsync ( panelId );

        return result;
    }
}

