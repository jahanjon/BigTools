using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierUpdateFileViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }
    public Guid FileId { get; set; }
    public FileType FileType { get; set; }
}