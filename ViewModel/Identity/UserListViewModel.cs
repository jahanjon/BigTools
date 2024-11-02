using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Identity;

namespace ViewModel.Identity;

public class UserListViewModel
{
    [JsonConverter(typeof(UserEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Mobile { get; set; }
}