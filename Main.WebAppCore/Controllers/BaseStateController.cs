using Main.Common.Models;
using Main.WebAppCore.DependentServices;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public partial class BaseController
{

    [NonAction]
    public void SetSessionImageFile (ImageFile imageFile,ITenantCacheService tenantCacheService)
    {
        // 🛡️ Guard Clause 1: Ensure the service was passed correctly
        if ( tenantCacheService == null )
        {
            throw new ArgumentNullException (nameof (tenantCacheService),"Cache service is not initialized.");
        }

        // 🛡️ Guard Clause 2: Ensure the image data isn't null
        if ( imageFile == null )
        {
            throw new ArgumentNullException (nameof (imageFile),"The uploaded image file object cannot be null.");
        }

        const string baseKey = "UploadedSessionImages";
        TimeSpan cacheDuration = TimeSpan.FromMinutes(30);

        // 🛡️ Guard Clause 3: Safely fetch or initialize the list
        // This ensures imageList is NEVER null when we reach the .Add() step
        if ( !tenantCacheService.TryGet<List<ImageFile>> (baseKey,out var imageList) || imageList == null )
        {
            imageList = [];
        }

        // Line 23 (or nearby): This will now safely execute
        imageList.Add (imageFile);

        // Update the cache
        tenantCacheService.Set (baseKey,imageList,cacheDuration);
    }


    protected List<ImageFile>? GetAllSessionImages (ITenantCacheService tenantCacheService)
    {
        if ( tenantCacheService.TryGet<List<ImageFile>>
        ("UploadedSessionImages",out var finalImages) && finalImages != null )
        {
            finalImages = finalImages.OrderBy (a => a.FileID).ToList ();
        }

        return [.. finalImages!];
    }

    protected bool DeleteSessionImage (int fileId,ITenantCacheService tenantCacheService)
    {
        const string baseKey = "UploadedSessionImages";
        TimeSpan cacheDuration = TimeSpan.FromMinutes(30);

        // 1. Get the existing list from the cache
        if ( !tenantCacheService.TryGet<List<ImageFile>> (baseKey,out var imageList) || imageList == null )
        {
            return false;
        }

        // 2. Find and remove the matching file from the list
        // (Assuming ImageFile has a FileName or Id property)
        ImageFile? itemToRemove = imageList.FirstOrDefault(img => img.FileID == fileId);

        if ( itemToRemove != null )
        {
            _ = imageList.Remove (itemToRemove);

            // Optional: If the list is now empty, just clear the cache entirely to save memory
            if ( imageList.Count != 0 )
            {
                // 3. Set the modified list back to cache (This overwrites it and resets the timer)
                tenantCacheService.Set (baseKey,imageList,cacheDuration);
            }
            else
            {
                tenantCacheService.Clear (baseKey);
            }

            return true;
        }

        return false;
    }

    protected void ClearImageFileListSession (ITenantCacheService tenantCacheService) =>
        // Clear the cache afterwards so memory is freed up immediately
        tenantCacheService.Clear ("UploadedSessionImages");
}