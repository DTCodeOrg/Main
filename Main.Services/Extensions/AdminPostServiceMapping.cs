using DataTransferModel;
using Main.Model.Tenant;

namespace Services.Extensions;

public static class AdminPostServiceMappings
{
    public static List<AdminPostDisplayModel> MapListDataModel (
        List<AdminPost> listAdminPostEntities)
    {
        ArgumentNullException.ThrowIfNull (listAdminPostEntities);
        AdminPostDisplayModel objDataModel;

        List<AdminPostDisplayModel> listPostDataModel
            = new();

        listAdminPostEntities.ForEach (postEntity =>
        {
            objDataModel = new AdminPostDisplayModel ();

            objDataModel.AdminPostID = postEntity.AdminPostID;
            objDataModel.PosterName = postEntity.PosterName;
            objDataModel.PostTitle = postEntity.Title;
            objDataModel.PostType = postEntity.PostType;

            listPostDataModel.Add (objDataModel);

        });

        return listPostDataModel;
    }

    public static AdminPostDataModel MapAdminPostDataModel (AdminPost postEntity)
    {
        if ( postEntity == null )
        {
            return new AdminPostDataModel ();
        }

        List<AdminImageFileDataModel> objAdminDataModelListFiles = [];
        AdminImageFileDataModel objAdminImageFileDataModel;

        if ( postEntity.ListAdminImageFiles != null && postEntity.ListAdminImageFiles.Count > 0 )
        {
            postEntity.ListAdminImageFiles.ToList ().ForEach (fileEntity =>
            {
                objAdminImageFileDataModel = new AdminImageFileDataModel ()
                {
                    AdminImageFileID = fileEntity.AdminImageFileID,
                    FileContent = fileEntity.ImageFileContent!,
                    AdminPostID = fileEntity.AdminPostID,
                    FilePath = fileEntity.FilePath
                };

                objAdminDataModelListFiles.Add (objAdminImageFileDataModel);

            });
        }


        List<AdminPostCommentDataModel> objDataModelListComments = [];

        AdminPostCommentDataModel objCommentDataModel;

        if ( postEntity.ListAdminPostComments != null
            && postEntity.ListAdminPostComments.Count > 0 )
        {

            postEntity.ListAdminPostComments.ToList ().ForEach
            (commentEntity =>
            {

                objCommentDataModel = new AdminPostCommentDataModel ()
                {
                    AdminPostCommentID = commentEntity.AdminPostCommentID,
                    Comment = commentEntity.Comment,
                    AdminPostID = commentEntity.AdminPostID
                };

                objDataModelListComments.Add (objCommentDataModel);

            });
        }

        AdminPostDataModel objAdminPostDataModel = new()
        {
            AdminPostID = postEntity.AdminPostID,
            PosterName = postEntity.PosterName,
            PostTitle = postEntity.Title,
            PosterContactNumber = postEntity.PosterContactNumber,
            WebsiteUrl = postEntity.WebsiteUrl,
            ShortNote = postEntity.ShortNote,
            SearchTag = postEntity.SearchTag,
            PostType = postEntity.PostType,
            ListAdminPostFileImages = objAdminDataModelListFiles,
            ListAdminPostComments = objDataModelListComments
        };

        return objAdminPostDataModel;
    }

    public static AdminPost MapAdminPostEntity
    (
        AdminPostDataModel from,
        List<AdminImageFileDataModel> fromListImages
    )
    {
        AdminPost adminPostEntity = CreareAdminPostEntity ( from );

        List<AdminImageFile> objListFileEntity = MapAdminFileEntity(from);

        adminPostEntity.ListAdminImageFiles = objListFileEntity;
        adminPostEntity.ListAdminPostComments = new List<AdminPostComment> ();

        return adminPostEntity;
    }


    private static AdminPost CreareAdminPostEntity (AdminPostDataModel adminPostDataModel)
    {
        AdminPost adminPost = new( )
        {
            PosterName = adminPostDataModel.PosterName,
            Title = adminPostDataModel.PostTitle,
            PostType =     adminPostDataModel.PostType ,
            WebsiteUrl = adminPostDataModel.WebsiteUrl,
            SearchTag = adminPostDataModel.SearchTag,
            ShortNote = adminPostDataModel.ShortNote,
            ListAdminImageFiles = new List<AdminImageFile> ( ),
            ListAdminPostComments = new List<AdminPostComment> ( ),
            PosterContactNumber = adminPostDataModel.PosterContactNumber
        };

        adminPost.CreateParameters (adminPostDataModel.BaseDataModel);

        return adminPost;
    }

    private static List<AdminImageFile> MapAdminFileEntity (AdminPostDataModel adminPostDataModel)
    {
        List<AdminImageFile> objListFileEntity = [];

        AdminImageFile adminFileEntity;

        adminPostDataModel.ListAdminPostFileImages.ForEach (fileDataModel =>
        {

            adminFileEntity = new AdminImageFile ();

            objListFileEntity.Add (adminFileEntity);

        });

        return objListFileEntity;
    }

    public static AdminPost UpdateAdminPostEntityMapping
    (AdminPost adminPostEntity,AdminPostDataModel adminPostDataModel)
    {
        adminPostEntity.ModifyParameters (adminPostDataModel.BaseDataModel);

        List<AdminImageFile> newListFileEntities = [];
        AdminImageFile adminImageFile;

        newListFileEntities.AddRange (adminPostEntity.ListAdminImageFiles);

        adminPostDataModel.ListAdminPostFileImages.ForEach (fileDataModel =>
        {
            adminImageFile = new AdminImageFile ()
            {
                AdminPostID = adminPostDataModel.AdminPostID,
                AdminImageFileID = ( int ) fileDataModel.AdminImageFileID!,
                FilePath = fileDataModel.FilePath!,
                ImageFileContent = fileDataModel.FileContent
            };

            newListFileEntities.Add (adminImageFile);
        });


        List<AdminPostComment> newListcommentEntities = new();

        adminPostDataModel.ListAdminPostComments.ForEach (commentDataModel =>
        {
            AdminPostComment adminPostComment = new ()
            {
                AdminPostID = adminPostDataModel.AdminPostID,
                Comment = commentDataModel.Comment
            };

            newListcommentEntities.Add (adminPostComment);

        });

        adminPostEntity.PosterName = adminPostDataModel.PosterName;
        adminPostEntity.Title = adminPostDataModel.PostTitle;
        adminPostEntity.PosterContactNumber = adminPostDataModel.PosterContactNumber;
        adminPostEntity.WebsiteUrl = adminPostDataModel.WebsiteUrl;
        adminPostEntity.ShortNote = adminPostDataModel.ShortNote;
        adminPostEntity.SearchTag = adminPostDataModel.SearchTag;
        adminPostEntity.PostType = adminPostDataModel.PostType;
        adminPostEntity.ListAdminPostComments = newListcommentEntities;
        adminPostEntity.ListAdminImageFiles = newListFileEntities;
        adminPostEntity.AdminPostID = adminPostDataModel.AdminPostID;

        return adminPostEntity;
    }


}
