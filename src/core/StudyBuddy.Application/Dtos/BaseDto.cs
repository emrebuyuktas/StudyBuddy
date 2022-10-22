using System.Text.Json.Serialization;

namespace StudyBuddy.Application.Dtos;

public abstract class BaseDto
{
    [JsonPropertyName("a")]public string? AccessToken { get; set; }
    [JsonPropertyName("ex")]public DateTime? AccessTokenExpiration { get; set; } 
}