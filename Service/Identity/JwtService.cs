using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Settings;
using Domain.Entity.Identity;
using Domain.Service.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Service.Identity;

public class JwtService(
    IOptionsSnapshot<SiteSettings> settings,
    SignInManager<User> signInManager)
    : IJwtService
{
    private readonly SiteSettings _siteSetting = settings.Value;

    public async Task<AccessToken> GenerateAsync(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.EncryptKey);
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await _getClaimsAsync(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _siteSetting.JwtSettings.Issuer,
            Audience = _siteSetting.JwtSettings.Audience,
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

        return new AccessToken(securityToken);
    }

    private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
    {
        var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

        var list = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.MobilePhone, user.Mobile),
            new(securityStampClaimType, user.SecurityStamp)
        };

        var roles = await signInManager.UserManager.GetRolesAsync(user);
        list.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return list;
    }
}