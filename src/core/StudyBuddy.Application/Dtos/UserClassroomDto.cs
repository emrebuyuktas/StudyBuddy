using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Dtos;

public class UserClassroomDto
{
    public string ClassroomId { get; set; }
    public string UserName { get; set; }
    public string ClassroomName { get; set; }
    public TagDto Tag { get; set; }
}