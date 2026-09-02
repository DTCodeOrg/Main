using DataTransferModel;
using Main.Common;
using Main.WebAppCore.Helpers;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Models.Extensions;

public static class PageMapping
{
    public static List<PageDisplayViewModel> PageDisplayMapping (List<PageDisplayDataModel> listPageDisplayDataModel,string company)
    {
        List<PageDisplayViewModel> listPageDisplayViewModels = [];

        PageDisplayViewModel pageDisplayViewModel;

        listPageDisplayDataModel.ForEach (dataModel =>
        {
            pageDisplayViewModel = new PageDisplayViewModel
            {
                PageID = dataModel.PageID,

                PageName = ListEnum.GetPageDescription (dataModel.EnumPublicPage),

                CompanyName = company
            };

            listPageDisplayViewModels.Add (pageDisplayViewModel);
        });

        return listPageDisplayViewModels;
    }

    public static List<PostSelectViewModel> MapSelectPostViewModel (List<PostDataModel> listSelectProductsDataModels,Currency currency,IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        if ( listSelectProductsDataModels == null )
        {
            return new List<PostSelectViewModel> ();
        }

        List<PostSelectViewModel> listPostSelectViewModels = [];

        PostSelectViewModel postSelectViewModel;

        listSelectProductsDataModels.ForEach (dataModel =>
        {
            postSelectViewModel =
            new PostSelectViewModel (dataModel.EnumPostType,dataModel.RootID,dataModel.ImageFileID,
            dataModel.ImageOrderID)
            {
                ImageFileContent = dataModel.FileContent,
                FilePath = dataModel.FilePath,
                CategoryName = DropDownListItems.GetTextForCategoryId
                (dataModel.CategoryID,localizer,storeType)
            };
        });

        return listPostSelectViewModels;
    }

    public static PageViewModel MapPageViewModel (PageDataModel pageDataModel)
    {
        List<PanelViewModel>  listPanelViewModel = [];

        PanelViewModel panelViewModel;

        pageDataModel.ListPanels.ForEach (pagePanelDataModel =>
        {
            panelViewModel = new PanelViewModel
            {
                PageID = pagePanelDataModel.PageID,
                PanelID = pagePanelDataModel.PanelID,
                PanelTitle = pagePanelDataModel.PanelTitle ?? "",
                PanelTemplate = pagePanelDataModel.PanelTemplate,
                PageName = ListEnum.GetPageDescription (pageDataModel.EnumPublicPage),
                PanelPosition = pagePanelDataModel.PanelPosition
            };

            PostViewModel postViewModel;

            pagePanelDataModel.ListPosts.ForEach (panelPostDataModel =>
            {
                postViewModel = new PostViewModel
                {
                    PanelPostID = panelPostDataModel.PanelPostID,

                    ImageFileContent = panelPostDataModel.FileContent,

                    FilePath = panelPostDataModel.FilePath,

                    ImageFileID = panelPostDataModel.ImageFileID,

                    Price = panelPostDataModel.Price,

                    PageID = panelViewModel.PageID,

                    CategoryID
                        = panelPostDataModel.CategoryID.HasValue ? panelPostDataModel.CategoryID.Value : 0,

                    PanelID = panelViewModel.PanelID,

                    PostTitle = panelPostDataModel.PostTitle
                };

                panelViewModel.CreatePanelPost (postViewModel);

            });

            listPanelViewModel.Add (panelViewModel);

        });

        PageViewModel pageViewModel = new ()
        {
            ListPagePanels =
                listPanelViewModel.ToList<PanelViewModel> ( )
                .OrderBy ( a => a.PanelPosition ).ToList ( )
        };

        return pageViewModel;
    }
}