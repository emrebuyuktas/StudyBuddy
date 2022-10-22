
namespace StudyBuddy.Domain.Entities;

public class Classroom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Tag> Tags { get; set; }
    public virtual List<Message> Messages { get; set; }
    public virtual List<AppUser> Users { get; set; }

    
}