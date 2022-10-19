using System.Text.Json.Serialization;

namespace StudyBuddy.Application.Dtos;

public class UserDto
{
    [JsonPropertyName("i")]public string Id { get; set; }
    [JsonPropertyName("u")]public string UserName { get; set; }
    [JsonPropertyName("e")]public string Email { get; set; }
}