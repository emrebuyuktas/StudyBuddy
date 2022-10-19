namespace StudyBuddy.Application.Dtos;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public long RefreshTokenExpiration { get; set; }
}