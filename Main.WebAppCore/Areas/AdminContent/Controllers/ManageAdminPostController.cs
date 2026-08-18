using DataTransferModel;
using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

[Area ("AdminContent")]
[Authorize (Policy = "TenantAdmin")]
public class ManageAdminPostController: BaseController
{
    private readonly IAdminPostService _adminPostService;
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantCacheService _tenantCacheService;
    private readonly IStorageService _storageService;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public ManageAdminPostController (
        IAdminPostService adminPostService,
        ITenantSetter tenantSetter,
        ITenantCacheService tenantCacheService,
        IStorageService storageService,
        IWebHostEnvironment webHostEnvironment)
    {
        _adminPostService = adminPostService;
        _tenantSetter = tenantSetter;
        _tenantCacheService = tenantCacheService;
        _storageService = storageService;
        _webHostEnvironment = webHostEnvironment;
    }

    private void SetImageInDataModel (AdminPostDataModel adminPostDataModel)
    {
        List<ImageFile>? listSessionImageFiles = GetAllSessionImages(_tenantCacheService);
        List<AdminImageFileDataModel> listAdminPostDataModel = new();

        if ( listSessionImageFiles?.Count > 0 )
        {
            listSessionImageFiles.ForEach (imgFile =>
            {
                AdminImageFileDataModel adminImageFileDataModel= new ()
                {
                    FileContent = imgFile.FileContent,
                    AdminPostID = imgFile.PostID ?? 0,
                    AdminImageFileID = 0 ,
                    FileName = imgFile.FileName,
                    FilePath = imgFile.FilePath
                };

                adminImageFileDataModel.FilePath = _storageService.CopyFileToSiblingFolder (_webHostEnvironment,_tenantSetter.ResolvedTenantId,
                    _tenantSetter.HttpContextUserId,true,imgFile.FileName);

                listAdminPostDataModel.Add (adminImageFileDataModel);


            });

            adminPostDataModel.ListAdminPostFileImages = listAdminPostDataModel;
            ClearImageFileListSession (_tenantCacheService);
        }
    }

    public async Task<ActionResult> Index ()
    {
        try
        {
            List<AdminPostDisplayModel> listAdminPosts = await _adminPostService.GetAllAdminPosts();

            return View (model: AdminPostMapping
                .MapAdminPostDisplayViewModelList
                (listAdminPosts,_tenantSetter.CurrentTenant.TenantName));
        }
        catch
        {
            return View (new List<AdminPostDisplayViewModel> ());
        }
    }

    [HttpGet]
    public IActionResult NewContent ()
    {
        try
        {
            ClearImageFileListSession (_tenantCacheService);

            var objPostViewModel = new AdminPostViewModel
            {
                PageName = "Add Admin Post"
            };

            return View (objPostViewModel);
        }
        catch
        {
            return View (new AdminPostViewModel ());
        }
    }


    [HttpPost]
    [AutoValidateAntiforgeryToken]
    [Authorize (Policy = "TenantAdmin")]
    public async Task<IActionResult> SaveContent (AdminPostViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest (error: "Invalid model state");
        }

        try
        {
            AdminPostDataModel adminPostDataModel = AdminPostMapping.MapNewDataModel (collection);

            SetImageInDataModel (adminPostDataModel);

            bool result = await _adminPostService.SaveNewAdminPost( adminPostDataModel );

            string? redirectUrl = Url.Action("Index", "ManageAdminPost", new
            {
                Area = "AdminContent"
            });

            return Ok (new
            {
                success = result,urlGo = redirectUrl
            });
        }
        catch ( Exception ex )
        {
            return BadRequest (new
            {
                success = false,message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult> Edit (int id)
    {
        try
        {
            ClearImageFileListSession (_tenantCacheService);

            AdminPostDataModel adminPostDataModel =
            await _adminPostService.GetAdminPostForEditPostID (id);

            var adminPostViewModel = new AdminPostViewModel();

            AdminPostMapping.MapAdminPostViewModel (adminPostDataModel,adminPostViewModel);

            adminPostViewModel.ListAdminPostFileImages = AdminPostMapping.MapAdminImageFileViewModelList (adminPostDataModel.ListAdminPostFileImages);

            adminPostViewModel.PageName = "Edit Post";


            return View (adminPostViewModel);
        }
        catch
        {
            return View (new AdminPostViewModel ());
        }
    }


    [HttpPost]
    public async Task<IActionResult> Edit (AdminPostViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest (ModelState);
        }

        try
        {
            AdminPostDataModel adminPostDataModel = AdminPostMapping.MapAdminPostDataModel ( collection );

            SetImageInDataModel (adminPostDataModel);

            bool result = await _adminPostService.UpdateAdminPost(adminPostDataModel);

            string? urlGo = Url.Action("Index", "ManageAdminPost", new
            {
                Area = "AdminContent"
            });

            return Ok (new
            {
                success = result,urlGo = urlGo
            });
        }
        catch ( Exception ex )
        {
            return BadRequest (new
            {
                success = false,message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult> Details (int id)
    {
        try
        {
            AdminPostDataModel adminPostDataModel = await _adminPostService.GetAdminPostForEditPostID(id);

            AdminPostViewModel adminPostViewModel = new();

            AdminPostMapping.MapAdminPostViewModel (adminPostDataModel,adminPostViewModel);

            adminPostViewModel.ListAdminPostFileImages = AdminPostMapping.MapAdminImageFileViewModelList (adminPostDataModel.ListAdminPostFileImages);

            adminPostViewModel.PageName = "Post Details";

            return View (adminPostViewModel);
        }
        catch
        {
            return View (new AdminPostViewModel ());
        }
    }




    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UploadImage (IFormFile file)
    {
        if ( file == null )
        {

            return StatusCode (500,new
            {
                success = false,message = "An error occurred during upload."
            });
        }

        string? filePath = await _storageService.SaveSessionTenantProductFileAsync(webHostEnvironment,_tenantSetter.ResolvedTenantId,_tenantSetter.HttpContextUserId,true,file);

        ImageFile imageFile = ReadImage (file);
        imageFile.SessionRelativeFilePath = filePath;

        SetSessionImageFile (imageFile,_tenantCacheService);


        // Return 200 OK with a JSON success payload
        return Ok (new
        {
            success = true,message = "File uploaded successfully!"
        });

    }

    private ImageFile ReadImage (IFormFile file)
    {
        if ( file != null && file.FileName != null )
        {
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant ();

            if ( extension.Equals (".jpg",StringComparison.Ordinal)
                || extension.Equals (".jpeg",StringComparison.Ordinal)
                || extension.Equals (".png",StringComparison.Ordinal)
                || extension.Equals (".gif",StringComparison.Ordinal) )
            {

                using var memoryStream = new MemoryStream();

                file.CopyTo (memoryStream);

                byte[] imgByte = memoryStream.ToArray();

                ImageFile objFile = new()
                {
                    FileContent = imgByte,FileName = file.FileName,IsNew = true,PostID = 0
                };

                return objFile;
            }
        }

        return new ImageFile ();
    }

    [HttpGet]
    public IActionResult LoadImage ()
    {

        List<ImageFile>? imageFileList = GetAllSessionImages(_tenantCacheService)!;

        ImageFile imageFile = imageFileList?.LastOrDefault<ImageFile>()!;

        return PartialView ("_Image",imageFile);

    }

    [HttpDelete]
    [Authorize (Policy = "TenantAdmin")]
    public async Task<JsonResult> ImageRemove (int id,int postId)
    {
        try
        {
            bool result;

            if ( postId != 0 )
            {
                result = await _adminPostService.DeleteAdminPostImage (id,postId);
            }

            result = DeleteSessionImage (id,_tenantCacheService);

            return Json (new
            {
                success = result
            });
        }
        catch
        {
            return Json (new
            {
                errors = false
            });
        }
    }


    [Authorize (Policy = "TenantAdmin")]
    public async Task<ActionResult> Delete (int id)
    {
        try
        {
            var objAdminPostDataModel = await _adminPostService.GetAdminPostForEditPostID(id);

            AdminPostViewModel adminPostViewModel = new();
            adminPostViewModel.AdminPostID = objAdminPostDataModel.AdminPostID;

            return View (adminPostViewModel);
        }
        catch
        {
            return BadRequest (new
            {
                success = false
            });
        }
    }


    [HttpPost]
    [Authorize (Policy = "TenantAdmin")]
    public async Task<ActionResult> DeleteContent (int id,int fakeId)
    {
        try
        {
            bool result = await _adminPostService.DeleteAdminPost(id);

            if ( result )
            {
                return RedirectToAction ("Index");
            }

            return RedirectToAction ("Delete",new
            {
                id = id
            });

        }
        catch
        {
            return BadRequest (new
            {
                success = false
            });
        }
    }
}