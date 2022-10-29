namespace StudyBuddy.Domain.Entities;

public class UserClassroom
{
    public string UserId { get; set; }
    public Guid ClassroomId { get; set; }
    public AppUser AppUser { get; set; }
    public Classroom Classroom { get; set; }
    public DateTime JoinDate { get; set; }=DateTime.Now;
}