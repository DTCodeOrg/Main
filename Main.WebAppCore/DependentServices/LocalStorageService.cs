namespace Main.WebAppCore.DependentServices;


public interface IStorageService
{
    Task<string?> SaveTenantAssetAsync
   (IWebHostEnvironment webHostEnvironment,Guid tenantId,IFormFile file,string folderName);

    Task<string?> SaveSessionTenantProductFileAsync
    (IWebHostEnvironment webHostEnvironment,
        Guid tenantId,
        string userId,
        bool active,
        IFormFile file,
        string folderName);

    Task<string?> SaveTenantProductFileAsync
   (IWebHostEnvironment webHostEnvironment,
       Guid tenantId,
       string userId,
       bool active,
       IFormFile file);

    Task<string?> SaveTenantSessionAdsFileAsync
    (IWebHostEnvironment webHostEnvironment,
        Guid tenantId,
        string userId,
        bool active,
        IFormFile file);

    Task<string?> SaveTenantAdsFileAsync
   (IWebHostEnvironment webHostEnvironment,
       Guid tenantId,
       string userId,
       bool active,
       IFormFile file);

    string ProductCopyFileToSiblingFolder (
         IWebHostEnvironment webHostEnvironment,
         Guid tenantId,
         string userId,
         bool active,
         string sessionFileName);

    string AdsCopyFileToSiblingFolder (
       IWebHostEnvironment webHostEnvironment,
       Guid tenantId,
       string userId,
       bool active,
       string sessionFileName);
}

public class LocalStorageService: IStorageService
{
    private readonly IWebHostEnvironment _env;

    public LocalStorageService (IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string?> SaveTenantAssetAsync
    (IWebHostEnvironment webHostEnvironment,Guid tenantId,IFormFile file,string folderName)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, folderName);

        if ( !Directory.Exists (uploadsFolder) )
        {
            _ = Directory.CreateDirectory (uploadsFolder);
        }

        string uniqueFileName =
        Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

        string filePath = Path.Combine(uploadsFolder, uniqueFileName);


        using ( var fileStream = new FileStream (filePath,FileMode.Create) )
        {
            await file.CopyToAsync (fileStream);
        }

        string databaseRelativePath = $"/uploads/{uniqueFileName}";

        return databaseRelativePath;
    }

    public async Task<string?> SaveSessionTenantProductFileAsync
    (IWebHostEnvironment webHostEnvironment,
        Guid tenantId,
        string userId,
        bool active,
        IFormFile file,
        string folderName)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        string uploadsFolderSession = Path.Combine(webHostEnvironment.WebRootPath, "ProductsSession");
        string uploadsFolderProducts = Path.Combine(webHostEnvironment.WebRootPath, "Products");

        if ( !Directory.Exists (uploadsFolderSession) )
        {
            _ = Directory.CreateDirectory (uploadsFolderSession);
        }

        if ( !Directory.Exists (uploadsFolderProducts) )
        {
            _ = Directory.CreateDirectory (uploadsFolderProducts);
        }

        string uniqueSessionFileName =
        $"{active}:{tenantId}:{userId}:{"Product"}:{Path.GetFileName(file.FileName)}";

        string filePath = Path.Combine(uploadsFolderSession, uniqueSessionFileName);


        using ( var fileStream = new FileStream (filePath,FileMode.Create) )
        {
            await file.CopyToAsync (fileStream);
        }

        string databaseRelativePath = $"/ProductsSession/{uniqueSessionFileName}";

        return databaseRelativePath;
    }

    public async Task<string?> SaveTenantSessionAdsFileAsync
    (IWebHostEnvironment webHostEnvironment,
        Guid tenantId,
        string userId,
        bool active,
        IFormFile file)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        string uploadsFolderSession = Path.Combine(webHostEnvironment.WebRootPath, "AdminAdsSession");
        string uploadsFolderProducts = Path.Combine(webHostEnvironment.WebRootPath, "AdminAds");

        if ( !Directory.Exists (uploadsFolderSession) )
        {
            _ = Directory.CreateDirectory (uploadsFolderSession);
        }

        if ( !Directory.Exists (uploadsFolderProducts) )
        {
            _ = Directory.CreateDirectory (uploadsFolderProducts);
        }

        string uniqueProductFileName =
        $"{active}:{tenantId}:{userId}:{"Ads"}:{Path.GetFileName(file.FileName)}";

        string filePath = Path.Combine(uploadsFolderSession, uniqueProductFileName);


        using ( var fileStream = new FileStream (filePath,FileMode.Create) )
        {
            await file.CopyToAsync (fileStream);
        }


        return filePath;
    }

    public string ProductCopyFileToSiblingFolder (
        IWebHostEnvironment webHostEnvironment,
        Guid tenantId,
        string userId,
        bool active,
        string sessionFileName)
    {
        string uploadsFolderSession = Path.Combine(webHostEnvironment.WebRootPath, "ProductsSession");

        string siblingFolder = Path.Combine(webHostEnvironment.WebRootPath, "Products");

        string sourceFileName = $"{active}:{tenantId}:{userId}:{"Product"}:{sessionFileName}";

        string sourceFilePath = Path.Combine (uploadsFolderSession,sourceFileName);

        if ( !File.Exists (sourceFilePath) )
        {
            throw new FileNotFoundException ("The source file could not be found.",sourceFilePath);
        }

        if ( !Directory.Exists (siblingFolder) )
        {
            _ = Directory.CreateDirectory (siblingFolder);
        }

        var destinationFilePath = Path.Combine(siblingFolder, sessionFileName);

        File.Copy (sourceFilePath,destinationFilePath,overwrite: true);

        return destinationFilePath;
    }


    public string AdsCopyFileToSiblingFolder (
       IWebHostEnvironment webHostEnvironment,
       Guid tenantId,
       string userId,
       bool active,
       string sessionFileName)
    {
        string uploadsFolderSession = Path.Combine(webHostEnvironment.WebRootPath, "AdminAdsSession");

        string siblingFolder = Path.Combine(webHostEnvironment.WebRootPath, "AdminAds");

        string sourceFileName = $"{active}:{tenantId}:{userId}:{"Ads"}:{sessionFileName}";

        string sourceFilePath = Path.Combine (uploadsFolderSession,sourceFileName);

        if ( !File.Exists (sourceFilePath) )
        {
            throw new FileNotFoundException ("The source file could not be found.",sourceFilePath);
        }

        if ( !Directory.Exists (siblingFolder) )
        {
            _ = Directory.CreateDirectory (siblingFolder);
        }

        var destinationFilePath = Path.Combine(siblingFolder, sessionFileName);

        File.Copy (sourceFilePath,destinationFilePath,overwrite: true);

        return destinationFilePath;
    }
}