
namespace StudyBuddy.Domain.Entities;

public class Classroom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Tags Tag { get; set; }
    public List<Message> Messages { get; set; }
    //public virtual List<AppUser> Users { get; set; }
    public virtual ICollection<UserClassroom> Users { get; set; }
}