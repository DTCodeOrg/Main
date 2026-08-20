using Main.Common;
using Main.Common.Models;

namespace Main.WebAppCore.DependentServices;

public interface IStorageService
{
    Task<string> SaveTenantLogoAsync (Guid tenantId,IFormFile file);

    Task<ImageFile> SaveSessionFileAsync
    (Guid tenantId,string userId,IFormFile file,bool isProduct);

    ImageFile MoveFileToDestinationFolder
    (Guid tenantId,string userId,string fileName,bool product);
}

public class LocalStorageService: IStorageService
{
    private const string ChildFolderProducts = "Products";
    private const string ChildFolderAdminAds = "AdminAds";
    private readonly IWebHostEnvironment _env;
    private string SessionRootFolder = string.Empty;
    private string SessionChildFolderProduct  = string.Empty;
    private string SessionChildFolderAdminAds  = string.Empty;
    private string TenantLogos  = string.Empty;
    private string TenantProducts  = string.Empty;
    private string TenantAdminAds  = string.Empty;

    public LocalStorageService (IWebHostEnvironment env)
    {
        _env = env;

        SessionRootFolder = Path.Combine (env.WebRootPath,"TenantFileSessionRoot");

        TenantProducts = Path.Combine (env.WebRootPath,"TenantProducts");

        TenantAdminAds = Path.Combine (env.WebRootPath,"TenantAdminAds");

        TenantLogos = Path.Combine (env.WebRootPath,"TenantLogos");

        SessionChildFolderProduct = Path.Combine (SessionRootFolder,ChildFolderProducts);

        SessionChildFolderAdminAds = Path.Combine (SessionRootFolder,ChildFolderAdminAds);
    }

    public async Task<string> SaveTenantLogoAsync (Guid tenantId,IFormFile file)
    {
        if ( file == null || file.Length == 0 )
        {
            return string.Empty;
        }

        if ( !Directory.Exists (TenantLogos) )
        {
            _ = Directory.CreateDirectory (TenantLogos);
        }

        string uniqueLogoFileName = $"{tenantId}_{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

        string logoFilePath = Path.Combine(TenantLogos, uniqueLogoFileName);

        using var fileStream = new FileStream (logoFilePath,FileMode.Create);

        await file.CopyToAsync (fileStream);

        string urlFile = $"/TenantLogos/{uniqueLogoFileName}";

        return urlFile;
    }

    public async Task<ImageFile> SaveSessionFileAsync
    (Guid tenantId,string userId,IFormFile file,bool isProduct)
    {
        if ( file == null || file.Length == 0 )
        {
            return new ImageFile ();
        }

        CreateFolders ("Products","AdminAds");

        if ( isProduct )
        {
            string sessionProductFileName
            = $"{tenantId}-{"Product"}-{userId}-{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            string filePathProduct
            = Path.Combine(SessionChildFolderProduct,sessionProductFileName);

            using ( var fileStream
            = new FileStream (filePathProduct,FileMode.Create) )
            {
                await file.CopyToAsync (fileStream);
            }

            ImageFile imageFile = new()
            {
                SessionFilePath = $"/TenantFileSessionRoot/Products/{sessionProductFileName}",
                FileName = sessionProductFileName,
                PostType = EnumPostType.Product,
                RelativeFilePath = $"/TenantFileSessionRoot/Products/{sessionProductFileName}"
            };
            return imageFile;
        }
        else
        {
            string sessionAdminAdFileName
            = $"{tenantId}-{"AdminAd"}-{userId}-{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            string filePathAdminAd
            = Path.Combine(SessionChildFolderAdminAds,sessionAdminAdFileName);

            using ( var fileStream = new FileStream (filePathAdminAd,FileMode.Create) )
            {
                await file.CopyToAsync (fileStream);
            }

            ImageFile imageFile = new()
            {
                SessionFilePath = $"/TenantFileSessionRoot/AdminAds/{sessionAdminAdFileName}",
                FileName = sessionAdminAdFileName,
                PostType = EnumPostType.AdSpace,
                RelativeFilePath = $"/TenantFileSessionRoot/AdminAds/{sessionAdminAdFileName}"
            };
            return imageFile;
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

    public ImageFile MoveFileToDestinationFolder
    (Guid tenantId,string userId,string fileName,bool product)
    {
        if ( product )
        {
            string sourceFolderFileFull = Path.Combine(SessionChildFolderProduct,fileName);
            string destFolderFileFull = Path.Combine(TenantProducts,fileName);
            if ( File.Exists (sourceFolderFileFull) )
            {
                File.Move (sourceFolderFileFull,destFolderFileFull,overwrite: true);
                ImageFile imageFile = new ()
                {
                    FileName = fileName,
                    RelativeFilePath = $"/TenantProducts/{fileName}"
                };
                return imageFile;
            }
            return new ImageFile ();
        }
        else
        {
            string sourceFolderFileFull = Path.Combine(SessionChildFolderAdminAds,fileName);
            string destFolderFilePath = Path.Combine(TenantAdminAds,fileName);
            if ( File.Exists (sourceFolderFileFull) )
            {
                File.Move (sourceFolderFileFull,destFolderFilePath,overwrite: true);
                ImageFile imageFile = new ()
                {
                    FileName = fileName,
                    RelativeFilePath = $"/TenantAdminAds/{fileName}"
                };
                return imageFile;
            }
            return new ImageFile ();
        }
    }
}