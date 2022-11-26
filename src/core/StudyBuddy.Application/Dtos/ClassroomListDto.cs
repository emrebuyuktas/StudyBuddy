

using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Dtos;

public class ClassroomListDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<TagDto> Tags { get; set; }
}