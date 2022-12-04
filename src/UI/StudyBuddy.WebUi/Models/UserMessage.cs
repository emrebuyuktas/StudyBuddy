namespace StudyBuddy.WebUi.Models;

public class UserMessage
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreateDateTime { get; set; }
}