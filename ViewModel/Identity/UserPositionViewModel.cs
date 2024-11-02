using ViewModel.Profile;

namespace ViewModel.Identity;

public class UserPositionViewModel
{
    public bool IsAdmin { get; set; }
    public List<ShopperKeyValueViewModel> Shoppers { get; set; }
    public List<SupplierKeyValueViewModel> Suppliers { get; set; }
}