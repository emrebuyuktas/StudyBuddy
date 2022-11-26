using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Dtos;

public class UserClassroomDto
{
    public string UserName { get; set; }
    public string ClassroomName { get; set; }
    public List<TagDto> Tags { get; set; }
}