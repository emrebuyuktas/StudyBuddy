
namespace StudyBuddy.Domain.Entities;

public class Classroom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Message> Messages { get; set; }
    //public virtual List<AppUser> Users { get; set; }
    public virtual List<UserClassroom> Users { get; set; } = new();
    public Tag Tag { get; set; }
}