using System.Text.Json.Serialization;

namespace StudyBuddy.WebUi.Models;

public class TagDto
{
    [JsonPropertyName("n")]public string Name { get; set; }
}