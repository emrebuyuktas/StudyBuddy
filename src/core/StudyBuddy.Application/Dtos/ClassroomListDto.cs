

using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Dtos;

public class ClassroomListDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TagDto Tag { get; set; }
    public int MemberCount { get; set; }
}