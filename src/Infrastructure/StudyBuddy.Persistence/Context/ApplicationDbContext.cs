using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Context;

public class ApplicationDbContext:  IdentityDbContext<AppUser, IdentityRole, string>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserClassroom> UserClassrooms { get; set; }
    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        base.SaveChanges();
    }
}