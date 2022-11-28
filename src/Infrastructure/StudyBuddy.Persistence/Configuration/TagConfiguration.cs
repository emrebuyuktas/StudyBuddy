using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.HasMany<AppUser>(x => x.Users).WithMany(y => y.Tags);
        builder.HasMany<Classroom>(x => x.Classrooms).WithOne(y => y.Tag);
    }
}