using System.Text.Json.Serialization;

namespace StudyBuddy.WebUi.Models;

public class UserDto
{
    public string userName { get; set; }
    public string email { get; set; }

    public string? accessToken { get; set; } 
    
    public DateTime? accessTokenExpiration { get; set; } 
}