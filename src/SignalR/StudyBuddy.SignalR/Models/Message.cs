namespace StudyBuddy.SignalR.Models;

public class Message
{
    public string GroupId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}