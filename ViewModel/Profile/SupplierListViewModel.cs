using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierListViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Name { get; set; }
    public string NaionalCode { get; set; }
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
}