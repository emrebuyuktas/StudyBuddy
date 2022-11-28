using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Entities.MongoDb;

namespace StudyBuddy.Application.Dtos;

public class ClassroomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TagDto Tag { get; set; }
    public List<MessageDto> Messages { get; set; }
    public List<UserDto> AppUsers { get; set; }
}