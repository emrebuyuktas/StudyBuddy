namespace StudyBuddy.Application.Dtos;

public class MessageDto
{
    public UserDto User { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public ClassroomDto Classroom { get; set; }
}