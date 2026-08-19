namespace Main.WebAppCore.DependentServices;

public interface IStorageService
{
    Task<string?> SaveTenantLogoAsync (Guid tenantId,IFormFile file);

    Task<string?> SaveSessionFileAsync
    (Guid tenantId,string userId,IFormFile file,bool isProduct);

    string CopyFileToDestinationFolder (
       Guid tenantId,
       string userId,
       string fullPathFile,
       bool product);
}

public class LocalStorageService: IStorageService
{
    private const string ChildFolderProducts = "\\Products";
    private const string ChildFolderAdminAds = "\\AdminAds";
    private readonly IWebHostEnvironment _env;

    private string SessionRootFolder
    {
        get; set;
    }
    private string SessionChildFolderProduct
    {
        get; set;
    }
    private string SessionChildFolderAdminAds
    {
        get; set;
    }
    private string TenantLogos
    {
        get; set;
    }
    private string TenantProducts
    {
        get; set;
    }
    private string TenantAdminAds
    {
        get; set;
    }

    public LocalStorageService (IWebHostEnvironment env)
    {
        _env = env;

        SessionRootFolder = Path.Combine (env.WebRootPath,"TenantFileSessionRoot");
        TenantProducts = Path.Combine (env.WebRootPath,"TenantProducts");
        TenantAdminAds = Path.Combine (env.WebRootPath,"TenantAdminAds");
        TenantLogos = Path.Combine (env.WebRootPath,"TenantLogoUploads");
        SessionChildFolderProduct = SessionRootFolder + ChildFolderProducts;
        SessionChildFolderAdminAds = SessionRootFolder + ChildFolderAdminAds;
    }

    public async Task<string?> SaveTenantLogoAsync (Guid tenantId,IFormFile file)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        if ( !Directory.Exists (TenantLogos) )
        {
            _ = Directory.CreateDirectory (TenantLogos);
        }

        string uniqueLogoFileName =
        $"{Guid.NewGuid().ToString()}:{tenantId.ToString()}:{Path.GetFileName(file.FileName)}";

        string logoFilePath = Path.Combine(TenantLogos, uniqueLogoFileName);


        using ( var fileStream = new FileStream (logoFilePath,FileMode.Create) )
        {
            await file.CopyToAsync (fileStream);
        }

        string logoFileRelativePath = $"/{TenantLogos}/{uniqueLogoFileName}";

        return logoFileRelativePath;
    }

    public async Task<string?> SaveSessionFileAsync
    (Guid tenantId,string userId,IFormFile file,bool isProduct)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        CreateFolders ("Products","AdminAds");

        if ( isProduct )
        {
            string sessionProductFileName =
                 $"{tenantId}:{"Product"}:{userId}:{Guid.NewGuid().ToString()}:{Path.GetFileName(file.FileName)}";

            string filePathProduct = Path.Combine (SessionChildFolderProduct,sessionProductFileName);

            using ( var fileStream = new FileStream (filePathProduct,FileMode.Create) )
            {
                await file.CopyToAsync (fileStream);
            }

            return filePathProduct;
        }
        else
        {
            string sessionAdminAdFileName =
                 $"{tenantId}:{"AdminAd"}:{userId}:{Guid.NewGuid().ToString()}:{Path.GetFileName(file.FileName)}";

            string filePathAdminAd = Path.Combine (SessionChildFolderAdminAds,sessionAdminAdFileName);

            using ( var fileStream = new FileStream (filePathAdminAd,FileMode.Create) )
            {
                await file.CopyToAsync (fileStream);
            }

            return filePathAdminAd;
        }
    }

    private void CreateFolders (string baseFolderProduct,string baseFolderAds)
    {
        if ( !Directory.Exists (SessionRootFolder) )
        {
            _ = Directory.CreateDirectory (SessionRootFolder);
        }

        if ( !Directory.Exists (SessionChildFolderProduct) )
        {
            _ = Directory.CreateDirectory (SessionChildFolderProduct);
        }

        if ( !Directory.Exists (SessionChildFolderAdminAds) )
        {
            _ = Directory.CreateDirectory (SessionChildFolderAdminAds);
        }

        if ( !Directory.Exists (TenantProducts) )
        {
            _ = Directory.CreateDirectory (TenantProducts);
        }

        if ( !Directory.Exists (TenantAdminAds) )
        {
            _ = Directory.CreateDirectory (TenantAdminAds);
        }
    }

    public string CopyFileToDestinationFolder (
       Guid tenantId,
       string userId,
       string fullPathFile,
       bool product)
    {
        string fileName = Path.GetFileName(fullPathFile);

        if ( product )
        {
            string destFolderFileFull = Path.Combine(TenantProducts , fileName);

            if ( File.Exists (fullPathFile) )
            {
                File.Move (fullPathFile,destFolderFileFull,overwrite: true);
            }

            return $"/{TenantProducts}/{fileName}";
        }
        else
        {
            string destFolderFileFull = Path.Combine(TenantAdminAds , fileName);

            if ( File.Exists (fullPathFile) )
            {
                File.Move (fullPathFile,destFolderFileFull,overwrite: true);
            }

            return $"/{TenantAdminAds}/{fileName}";
        }
    }
}