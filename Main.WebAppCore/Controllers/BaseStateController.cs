using DataTransferModel;
using Main.WebAppCore.DependentServices;

namespace Main.WebAppCore;

public partial class BaseController
{
    protected void SetSessionImageFile (ImageFile imageFile,
        ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>?> ("ImageFileList",out var listImageFile);

        if ( listImageFile == null )
        {
            List<ImageFile>? listNewImageFile = new();
            imageFile.FileID = 1;
            listNewImageFile.Add (imageFile);

            tenantCacheService.Set<List<ImageFile>?> ("ImageFileList",listNewImageFile
                ,new System.TimeSpan (1200));
        }
        else
        {
            listImageFile = listImageFile.OrderBy (a => a.FileID).ToList ();
            int currentId = listImageFile.Last ( ).FileID;
            currentId += 1;

            imageFile.FileID = currentId;

            listImageFile.Add (imageFile);

            tenantCacheService.Set<List<ImageFile>?> ("ImageFileList",listImageFile
               ,new System.TimeSpan (1200));
        }
    }

    protected List<ImageFile> GetAllSessionImages (ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>?> ("ImageFileList",out var listImageFile);

        if ( listImageFile != null )
        {
            return listImageFile.ToList ();
        }

        return new List<ImageFile> ();
    }

    protected bool RemoveSessionImageFile (int imageFileId,ITenantCacheService tenantCacheService)
    {
        _ = tenantCacheService.TryGet<List<ImageFile>?> ("ImageFileList",out var listImageFile);

        if ( listImageFile == null )
        {
            return false;
        }

        ImageFile? imageFile =
        listImageFile.Where(a => a.FileID == imageFileId).FirstOrDefault();

        if ( imageFile == null )
        {
            return false;
        }

        bool result = listImageFile.Remove (imageFile);

        tenantCacheService.Set<List<ImageFile>?>
        ("ImageFileList",listImageFile,new System.TimeSpan (1200));

        return true;
    }

    protected void ClearImageFileListSession (ITenantCacheService tenantCacheService)
    {
        tenantCacheService.Clear ("ImageFileList");
    }
}