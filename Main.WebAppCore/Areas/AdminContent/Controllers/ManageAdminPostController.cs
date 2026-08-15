using DataTransferModel;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

[Area ("AdminContent")]
[Authorize (Policy = "TenantAdmin")]
public class ManageAdminPostController: BaseController
{
    private readonly IAdminPostService _adminPostService;
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantCacheService _tenantCacheService;

    public ManageAdminPostController (
        IAdminPostService adminPostService,
        ITenantSetter tenantSetter,
        ITenantCacheService tenantCacheService)
    {
        _adminPostService = adminPostService;
        _tenantSetter = tenantSetter;
        _tenantCacheService = tenantCacheService;
    }

    private void SetImageInDataModel (AdminPostDataModel adminPostDataModel)
    {
        List<ImageFile> listSessionImageFiles = GetAllSessionImages(_tenantCacheService);

        listSessionImageFiles.ForEach (imgFile =>
        {
            AdminImageFileDataModel adminImageFileDataModel= new(  )
            {
                ImageFileContent = imgFile.FileContent,
                AdminPostID = imgFile.PostID ?? 0,
                AdminImageFileID = 0
            };

            new List<AdminImageFileDataModel> ().Add (adminImageFileDataModel);
        });

        adminPostDataModel.ListAdminPostFileImages = new List<AdminImageFileDataModel> ();

        ClearImageFileListSession (_tenantCacheService);
    }



    public async Task<ActionResult> Index ()
    {
        try
        {
            List<AdminPostDisplayModel> listAdminPosts = await _adminPostService.GetAllAdminPosts();

            return View (model: AdminPostMapping
                .MapAdminPostDisplayViewModelList (
                listAdminPosts,
                _tenantSetter.CurrentTenant.TenantName));
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
            SetImageInDataModel (AdminPostMapping.MapNewDataModel (collection));

            bool result = await _adminPostService.SaveNewAdminPost( postDataModel: AdminPostMapping.MapNewDataModel ( collection ) );

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
                await _adminPostService.GetAdminPostForEditPostID(id);


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
    public JsonResult UploadImage (IFormFile file)
    {
        if ( file != null && file.Length > 0 )
        {
            if ( file == null || file.Length > AppSettings.Current.PostImageSize )
            {
                return Json (new
                {
                    success = false
                });
            }
            else
            {
                ImageFile imageFile = ReadImage ( file );

                if ( imageFile.IsNew )
                {
                    SetSessionImageFile (imageFile,_tenantCacheService);
                }

                return Json (new
                {
                    success = true
                });
            }
        }

        return Json (new
        {
            success = false
        });
    }

    private ImageFile ReadImage (IFormFile file)
    {
        if ( !string.IsNullOrEmpty (file.ContentType) && file.FileName != null )
        {
            string extension = Path.GetExtension(file.FileName).ToLower();

            if ( extension.Equals (".jpg") || extension.Equals (".jpeg")
                || extension.Equals (".png") || extension.Equals (".gif") )
            {

                using var memoryStream = new MemoryStream();
                file.CopyTo (memoryStream);
                byte[] imgByte = memoryStream.ToArray();

                ImageFile objFile = new()
                {
                    FileContent = imgByte,
                    IsNew = true,
                    PostID = 0
                };

                return objFile;
            }
        }
        return new ImageFile ();
    }

    [HttpGet]
    public PartialViewResult LoadImage ()
    {
        try
        {
            List<ImageFile> imageFileList = GetAllSessionImages(_tenantCacheService);

            if ( imageFileList == null || imageFileList.Count == 0 )
            {
                return PartialView ("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml",new ImageFile ());
            }

            ImageFile imageFile = imageFileList.Last();

            return PartialView ("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml",imageFile);
        }
        catch
        {
            return PartialView ("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml",new ImageFile ());
        }
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

            result = RemoveSessionImageFile (id,_tenantCacheService);

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