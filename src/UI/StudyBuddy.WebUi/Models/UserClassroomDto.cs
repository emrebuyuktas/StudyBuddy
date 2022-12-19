namespace StudyBuddy.WebUi.Models;

public class UserClassroomDto
{
    public string ClassroomId { get; set; }
    
    public string UserName { get; set; }
    public string ClassroomName { get; set; }
    public TagDto Tag { get; set; }
    public int UserCount { get; set; }
    
}