using System.Text.Json.Serialization;

namespace StudyBuddy.Application.Dtos;

public class TagDto
{
    [JsonPropertyName("n")]public string Name { get; set; }
}