using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierActivateViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Id { get; set; }
}