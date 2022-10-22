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
    TokenDto CreateToken(string Email,string UserId,string UserName);
}
public class TokenService: ITokenService
{
    private readonly CustomTokenOption _tokenOption;

    public TokenService(IOptions<CustomTokenOption> options)
    {
        _tokenOption = options.Value;
    }

    private string CreateRefreshToken()
    {
        var numberBytes=new Byte[32];
        using var rnd=RandomNumberGenerator.Create();
        rnd.GetBytes(numberBytes);
        return Convert.ToBase64String(numberBytes);
    }

    private IEnumerable<Claim> GetClaims(string Email,string UserId,string UserName,List<string> audiences)
    {
        var userList=new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,UserId),
            new Claim(ClaimTypes.Email,Email),
            new Claim(ClaimTypes.Name,UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };
        userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return userList;
    }

    private SecurityKey GetSymmetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
    
    public TokenDto CreateToken(string Email,string UserId,string UserName)
    {
        var accessTokenExpiration=DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        var securityKey = GetSymmetricSecurityKey(_tokenOption.SecurityKey);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOption.Issuer, 
            expires: accessTokenExpiration,
            notBefore: DateTime.Now, 
            claims: GetClaims(Email,UserId,UserName, _tokenOption.Audience), 
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