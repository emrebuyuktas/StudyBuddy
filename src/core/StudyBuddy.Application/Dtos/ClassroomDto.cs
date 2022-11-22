using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Dtos;

public class ClassroomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Tags Tag { get; set; }
    public List<MessageDto> Messages { get; set; }
    public List<UserDto> AppUsers { get; set; }
}