namespace StudyBuddy.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Classroom> Classrooms { get; set; }
    }
}
