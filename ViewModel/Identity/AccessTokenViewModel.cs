namespace ViewModel.Identity;

public class AccessTokenViewModel
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public List<string> Roles { get; set; }
    public bool IsNewUser { get; set; }
    public bool IsActive { get; set; }
}