using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class UnitUpdateViewModel
{
    [JsonConverter(typeof(UnitEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Title { get; set; }
}