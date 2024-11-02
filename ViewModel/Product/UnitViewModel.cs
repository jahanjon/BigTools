using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class UnitViewModel
{
    [JsonConverter(typeof(UnitEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
}