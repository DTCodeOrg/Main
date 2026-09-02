using DataTransferModel;
using Main.Common;
using Main.Model.Identity;
using Main.Model.Tenant;

namespace Main.Services.Extensions;

public static class PageServiceMapping
{
    public static Panel CreatePanelEntity (PanelDataModel panelDataModel)
    {
        Panel panelEntity =
        new ( panelDataModel.PageID, panelDataModel.PanelTemplate, panelDataModel.PanelTitle );

        return panelEntity;
    }

    public static List<Post> CreateListPostEntity (PanelDataModel panelDataModel)
    {
        List<Post>  listPosts = [];
        Post post;
        int order = 1;

        panelDataModel.ListPosts.ForEach (postDataModel =>
        {
            post = new Post (postDataModel.EnumPostType,postDataModel.Price!,postDataModel.RootID)
            {
                FileContent = postDataModel.FileContent,
                FilePath = postDataModel.FilePath,
                Title = postDataModel.PostTitle,
                Order = order
            };

            listPosts.Add (post);

            order += 1;
        });

        return listPosts;
    }


    public static List<PostDataModel> GetPostDataModels (List<Product> listProducts)
    {
        if ( listProducts == null )
        {
            return new List<PostDataModel> ();
        }

        List<PostDataModel> listPanelPostDataModel = [];

        PostDataModel panelPostDataModel;

        int id = 1;

        listProducts.ForEach (productEntity =>
        {
            productEntity.ListImageFiles.ToList ().ForEach (file =>
            {
                panelPostDataModel = new PostDataModel
                {
                    PanelPostID = id,
                    EnumPostType = EnumPostType.Product,
                    RootID = productEntity.ProductID,
                    CategoryID = productEntity.CategoryID,
                    SubCategoryID = productEntity.SubCategoryID,
                    Price = productEntity.Price,
                    PostTitle = productEntity.ProductName,
                    ProductOwner = productEntity.ProductOwner,
                    FileContent = file.FileContent!,
                    ImageFileID = file.ProductImageFileID,
                    FilePath = file.FilePath
                };

                id += 1;

                listPanelPostDataModel.Add (panelPostDataModel);
            });
        });

        return listPanelPostDataModel.ToList ();
    }

    public static PageDataModel MapPageDataModel (Page pageEntity)
    {
        if ( pageEntity != null )
        {
            List<Panel> listPanels = pageEntity.ListPanels.ToList ();

            PageDataModel pageDataModel = new( );

            List<PanelDataModel> listPanelDataModel  = [];

            PanelDataModel panelDataModel;

            PostDataModel postDataModel;

            listPanels.ToList<Panel> ().OrderBy (a => a.PanelPosition).ToList ().ForEach (panel =>
            {
                panelDataModel = new PanelDataModel
                {
                    PageID = panel.PageID,
                    PanelID = panel.PanelID,
                    PanelTemplate = panel.PanelTemplate,
                    PanelTitle = panel.PanelTitle,
                    PanelPosition = panel.PanelPosition
                };

                panel.ListPosts.ToList ().ForEach (panelPost =>
                {
                    postDataModel = new PostDataModel ()
                    {
                        PanelPostID = panelPost.PostID,
                        PostTitle = panelPost.Title,
                        ProductOwner = panelPost.ProductOwner,
                        Price = panelPost.Price,
                        FileContent = panelPost.FileContent,
                        FilePath = panelPost.FilePath,
                        PostOrder = panelPost.Order,
                        PageID = panelDataModel.PageID,
                        CategoryID = panelPost.CategoryID,
                        SubCategoryID = panelPost.SubCategoryID
                    };

                    panelDataModel.CreatePost (postDataModel);
                });

                int actualCount  = panelDataModel.ListPosts.ToList().Count;

                EnumIsValidTemplate validTemplate =
                ValidationRelated.IsValidTemplate ( actualCount, panelDataModel.PanelTemplate );

                if ( validTemplate == EnumIsValidTemplate.ExactMatchValid )
                {
                    pageDataModel.CreatePanel (panelDataModel);
                }

                if ( validTemplate == EnumIsValidTemplate.GreaterMatchValid )
                {
                    int count = ValidationRelated.GetPostCount(panelDataModel.PanelTemplate);

                    List<PostDataModel> listPosts =
                    panelDataModel.ListPosts.Take(count).ToList();

                    panelDataModel.ListPosts = listPosts;

                    pageDataModel.CreatePanel (panelDataModel);
                }
            });

            return pageDataModel;
        }

        return new PageDataModel ();
    }

    public static List<PostDataModel> GetPostDataModels (List<AdminPost> listAdminPosts)
    {
        if ( listAdminPosts == null )
        {
            return new List<PostDataModel> ();
        }

        List<PostDataModel> listPostDataModels = new();

        PostDataModel postDataModel;

        int id = 1;

        listAdminPosts.ForEach (adminPostEntity =>
        {
            adminPostEntity.ListAdminImageFiles.ToList ().ForEach (fileEntity =>
            {
                postDataModel = new PostDataModel
                {
                    PanelPostID = id,
                    RootID = adminPostEntity.AdminPostID,
                    EnumPostType = adminPostEntity.PostType,
                    PostTitle = adminPostEntity.Title,
                    FileContent = fileEntity.ImageFileContent!,
                    WebsiteUrl = adminPostEntity.WebsiteUrl,
                    FilePath = fileEntity.FilePath
                };

                listPostDataModels.Add (postDataModel);

                id += 1;
            });
        });

        return listPostDataModels.ToList ();
    }
}
