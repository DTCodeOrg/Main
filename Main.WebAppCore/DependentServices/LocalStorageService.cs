namespace Main.WebAppCore.DependentServices;

public interface IStorageService
{
    Task<string?> SaveTenantAssetAsync (Guid tenantId,IFormFile file,string folderName);
}


public class LocalStorageService: IStorageService
{
    private readonly IWebHostEnvironment _env;

    public LocalStorageService (IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string?> SaveTenantAssetAsync (Guid tenantId,IFormFile file,string folderName)
    {
        if ( file == null || file.Length == 0 )
        {
            return null;
        }

        // Group assets by Tenant ID directory securely
        string uploadDir = Path.Combine(_env.WebRootPath, "uploads", tenantId.ToString(), folderName);

        if ( !Directory.Exists (uploadDir) )
        {
            _ = Directory.CreateDirectory (uploadDir);
        }

        // Use a unique file name to avoid browser caching issues on updates
        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string filePath = Path.Combine(uploadDir, uniqueFileName);

        using ( var stream = new FileStream (filePath,FileMode.Create) )
        {
            await file.CopyToAsync (stream);
        }

        return uniqueFileName;
    }
}
