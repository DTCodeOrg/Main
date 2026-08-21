using Main.Common.Models;
using Main.WebAppCore.DependentServices;

namespace Main.WebAppCore.Controllers;

public partial class BaseController
{
    public void SetSessionImageFile (ImageFile imageFile,ITenantCacheService tenantCacheService)
    {
        if ( tenantCacheService == null )
        {
            throw new ArgumentNullException
            (nameof (tenantCacheService),"Cache service is not initialized.");
        }

        if ( imageFile == null )
        {
            throw new ArgumentNullException
            (nameof (imageFile),"The uploaded image file object cannot be null.");
        }

        const string baseKey = "UploadedSessionImages";
        TimeSpan cacheDuration = TimeSpan.FromMinutes(30);

        if ( !tenantCacheService.TryGet<List<ImageFile>> (baseKey,out var imageList) || imageList == null )
        {
            imageList = [];
        }

        imageList.Add (imageFile);
        tenantCacheService.Set (baseKey,imageList,cacheDuration);
    }

    protected List<ImageFile>? GetAllSessionImages (ITenantCacheService tenantCacheService)
    {
        if ( tenantCacheService.TryGet<List<ImageFile>>
        ("UploadedSessionImages",out var finalImages) && finalImages != null )
        {
            finalImages = finalImages.OrderBy (a => a.FileID).ToList ();
        }
        return finalImages;
    }

    protected bool DeleteSessionImage (string fileName,ITenantCacheService tenantCacheService)
    {
        const string baseKey = "UploadedSessionImages";
        TimeSpan cacheDuration = TimeSpan.FromMinutes(30);

        if ( !tenantCacheService.TryGet<List<ImageFile>> (baseKey,out var imageList) || imageList == null )
        {
            return false;
        }

        ImageFile? itemToRemove = imageList.FirstOrDefault(img => img.FileName == fileName);

        if ( itemToRemove != null )
        {
            bool result = imageList.Remove (itemToRemove);

            if ( imageList.Count > 0 )
            {
                tenantCacheService.Set (baseKey,imageList,cacheDuration);
            }
            else
            {
                tenantCacheService.Set (baseKey,imageList,cacheDuration);
            }

            return true;
        }

        return false;
    }

    protected void ClearImageFileListSession (ITenantCacheService tenantCacheService)
    {
        tenantCacheService.Clear ("UploadedSessionImages");
    }
}