using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierFileViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }
    public List<Guid> FileIds { get; set; }
}