

using StudyBuddy.Domain.Entities;
using StudyBuddy.WebUi.Models;

namespace StudyBuddy.Application.Dtos;

public class ClassroomListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TagDto> Tags { get; set; }
}