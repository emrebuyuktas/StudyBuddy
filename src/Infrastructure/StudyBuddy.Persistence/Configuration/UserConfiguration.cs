using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany<Classroom>(c => c.Classrooms).WithMany(u => u.Users);
        builder.HasMany<Message>(m => m.Messages).WithOne(u => u.User).HasForeignKey(k=>k.UserId);
        
    }
}