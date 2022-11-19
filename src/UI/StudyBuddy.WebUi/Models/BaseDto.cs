using System.Text.Json.Serialization;

namespace StudyBuddy.WebUi.Models;

public abstract class BaseDto
{
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; } 
}