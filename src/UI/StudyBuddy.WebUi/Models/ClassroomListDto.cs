

using StudyBuddy.Domain.Entities;
using StudyBuddy.WebUi.Models;

namespace StudyBuddy.Application.Dtos;

public class ClassroomListDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TagDto Tag { get; set; }
    public int UserCount { get; set; }
}