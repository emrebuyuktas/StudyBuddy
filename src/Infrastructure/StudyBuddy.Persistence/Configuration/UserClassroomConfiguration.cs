using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Configuration;

public class UserClassroomConfiguration :IEntityTypeConfiguration<UserClassroom>
{
    public void Configure(EntityTypeBuilder<UserClassroom> builder)
    {
        builder.HasKey(x => new { x.ClassroomId, x.UserId });
        builder.HasOne<AppUser>(x => x.AppUser).WithMany(y => y.Classrooms).HasForeignKey(x=>x.UserId);
        builder.HasOne<Classroom>(x => x.Classroom).WithMany(y => y.Users);
    }
}