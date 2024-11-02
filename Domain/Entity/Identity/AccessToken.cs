using System.IdentityModel.Tokens.Jwt;

namespace Domain.Entity.Identity;

public class AccessToken
{
    public AccessToken(JwtSecurityToken securityToken)
    {
        access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        token_type = "Bearer";
        expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
    }

    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public List<string> Roles { get; set; }
    public bool IsNewUser { get; set; }
    public bool IsActive { get; set; }
}