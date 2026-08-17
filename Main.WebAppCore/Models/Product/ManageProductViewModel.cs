namespace Main.WebAppCore.Models;

public class ManageProductViewModel: BaseViewModel
{
    public ManageProductViewModel ()
    {
        ListProduct = new List<ProductViewModel> ();
    }

    public List<ProductViewModel> ListProduct
    {
        get; set;
    }
}
