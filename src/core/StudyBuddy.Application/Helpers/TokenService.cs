using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Helpers;

public interface ITokenService
{
    TokenDto CreateToken(AppUser user);
}
public class TokenService: ITokenService
{
    private readonly UserManager<AppUser> _user;
    private readonly CustomTokenOption _tokenOption;

    public TokenService(IOptions<CustomTokenOption> options, UserManager<AppUser> user)
    {
        _tokenOption = options.Value;
        _user = user;
    }

    private string CreateRefreshToken()
    {
        var numberBytes=new Byte[32];
        using var rnd=RandomNumberGenerator.Create();
        rnd.GetBytes(numberBytes);
        return Convert.ToBase64String(numberBytes);
    }

    private IEnumerable<Claim> GetClaims(AppUser user,List<string> audiences)
    {
        var userList=new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };
        userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return userList;
    }

    private SecurityKey GetSymmetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
    
    public TokenDto CreateToken(AppUser user)
    {
        var accessTokenExpiration=DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        var securityKey = GetSymmetricSecurityKey(_tokenOption.SecurityKey);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOption.Issuer, 
            expires: accessTokenExpiration,
            notBefore: DateTime.Now, 
            claims: GetClaims(user, _tokenOption.Audience), 
            signingCredentials: signingCredentials);

        var handler=new JwtSecurityTokenHandler();

        var token=handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = _tokenOption.RefreshTokenExpiration
        };
        return tokenDto;
    }
}