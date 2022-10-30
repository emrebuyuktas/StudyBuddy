using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Interfaces;

public interface IApplicationDbContext 
{
    public DbSet<Domain.Entities.Classroom>Classrooms { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserClassroom> UserClassrooms { get; set; }
    
    Task SaveChangesAsync();
    void SaveChanges();
}