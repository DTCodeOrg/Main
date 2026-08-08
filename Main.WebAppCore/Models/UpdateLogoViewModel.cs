using System.ComponentModel.DataAnnotations;
using WebAppCore.ViewModel;

namespace Main.WebAppCore.Models;

public class UpdateLogoViewModel: BaseViewModel
{
    public UpdateLogoViewModel ()
    {
    }

    [Required (ErrorMessage = "Please select an image file.")]
    [Display (Name = "Select New Logo Image")]
    public IFormFile LogoFile { get; set; } = null!;

    public string? CurrentLogoFileName
    {
        get; set;
    }
}
