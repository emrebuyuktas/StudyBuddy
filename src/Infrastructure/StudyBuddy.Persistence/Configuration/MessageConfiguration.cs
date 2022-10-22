using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Content).HasMaxLength(600);
    }
}