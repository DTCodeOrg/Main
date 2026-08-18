using Main.Common.Models;
using Main.WebAppCore.DependentServices;

namespace Main.WebAppCore;

public partial class BaseController
{
    protected void SetSessionImageFile (ImageFile imageFile,
        ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>> ("ImageFileList",out var listImageFile);

        if ( listImageFile == null || listImageFile.Count == 0 )
        {
            List<ImageFile> listNewImageFile = new();
            imageFile.FileID = 1;
            imageFile.FileContent = imageFile.FileContent;
            imageFile.PostID = imageFile.PostID;
            listNewImageFile.Add (imageFile);

            tenantCacheService.Set<List<ImageFile>> ("ImageFileList",listNewImageFile
                ,new System.TimeSpan (1200));
        }
        else
        {
            listImageFile = listImageFile.OrderBy (a => a.FileID).ToList ();
            int currentId = listImageFile.Last ( ).FileID;
            currentId += 1;

            imageFile.FileID = currentId;
            imageFile.FileContent = imageFile.FileContent;
            imageFile.PostID = imageFile.PostID;

            listImageFile.Add (imageFile);

            tenantCacheService.Set<List<ImageFile>>
            ("ImageFileList",listImageFile,new System.TimeSpan (1200));

        }
    }

    protected List<ImageFile> GetAllSessionImages (ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>> ("ImageFileList",out var listImageFile);

        if ( listImageFile == null || listImageFile.Count == 0 )
        {
            listImageFile = new List<ImageFile> ();

            tenantCacheService.Set<List<ImageFile>>
           ("ImageFileList",listImageFile,new System.TimeSpan (1200));

            return listImageFile.ToList ();
        }
        else
        {
            listImageFile = listImageFile.OrderBy (a => a.FileID).ToList ();

            tenantCacheService.Set<List<ImageFile>>
            ("ImageFileList",listImageFile,new System.TimeSpan (1200));

            return listImageFile.ToList ();
        }
    }

    protected bool RemoveSessionImageFile (int imageFileId,ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>> ("ImageFileList",out var listImageFile);

        if ( listImageFile == null || listImageFile.Count == 0 )
        {
            listImageFile = new List<ImageFile> ();

            tenantCacheService.Set<List<ImageFile>>
           ("ImageFileList",listImageFile,new System.TimeSpan (1200));

            return false;
        }

        ImageFile? imageFile =
        listImageFile.Where(a => a.FileID == imageFileId).FirstOrDefault();

        if ( imageFile == null )
        {
            return false;
        }

        bool result = listImageFile.Remove (imageFile);
        listImageFile = listImageFile.OrderBy (a => a.FileID).ToList ();

        tenantCacheService.Set<List<ImageFile>>
        ("ImageFileList",listImageFile,new System.TimeSpan (1200));

        return true;
    }

    protected void ClearImageFileListSession (ITenantCacheService tenantCacheService)
    {
        tenantCacheService.Clear ("ImageFileList");
    }
}