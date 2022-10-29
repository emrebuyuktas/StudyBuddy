namespace StudyBuddy.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public AppUser User { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }=DateTime.Now;
    public Classroom Classroom { get; set; }
    public Guid ClassroomId { get; set; }
}