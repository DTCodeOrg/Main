using DataTransferModel;
using Main.Infrastructure.ICrosscuttingServices;
using Main.Services;
using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.Controllers;

[Authorize (Policy = "TenantAdmin")]
public class AdminController: Controller
{
    private readonly IAccountService _accountService;
    private readonly IEmailSenderService _emailSenderService;


    public AdminController (IAccountService accountService,
        IEmailSenderService emailSenderService)
    {
        _accountService = accountService;

        _emailSenderService = emailSenderService;
    }

    [HttpGet]
    public async Task<IActionResult> UserDashboard ()
    {
        List<ApplicationUserDataModel>? listIdentityUserDataModel = await _accountService.Users ( );

        List<IdentityUserViewModel> listIdentityUserDisplayViewModels
            = new();

        IdentityUserViewModel identityUserDisplayViewModel;

        listIdentityUserDataModel?.ForEach (identityUserDataModel =>
        {
            identityUserDisplayViewModel = new IdentityUserViewModel
            {
                UserId = identityUserDataModel.Id,
                UserName = identityUserDataModel.UserName,
                LockoutEnd = identityUserDataModel.LockoutEnd
            };

            listIdentityUserDisplayViewModels.Add (identityUserDisplayViewModel);
        });

        return View (listIdentityUserDisplayViewModels);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnlockUser (string userId)
    {
        bool success = await _accountService.UnlockUser ( userId );

        if ( !success )
        {
            TempData["ErrorMessage"] = $"Failed to unlock account for user with ID {userId}.";
        }

        string userName = await _emailSenderService.SendEmailAsync ( userId );

        TempData["SuccessMessage"] = $"Unlocked and notified {userName}.";

        return RedirectToAction (nameof (UserDashboard));
    }
}
