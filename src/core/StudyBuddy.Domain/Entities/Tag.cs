namespace StudyBuddy.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Classroom> Classrooms { get; set; }
    public ICollection<AppUser> Users { get; set; }
}