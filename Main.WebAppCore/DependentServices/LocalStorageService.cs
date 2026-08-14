namespace Main.WebAppCore.DependentServices;

public interface IStorageService
{
    Task<string?> SaveTenantAssetAsync
    (IWebHostEnvironment webHostEnvironment,Guid tenantId,IFormFile file,string folderName);
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
}
