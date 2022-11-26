using System.Text.Json.Serialization;

namespace StudyBuddy.Application.Dtos;

public class UserDto: BaseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public string Id { get; set; }
}