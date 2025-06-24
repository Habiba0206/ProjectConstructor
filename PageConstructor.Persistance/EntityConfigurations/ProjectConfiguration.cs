using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.HasMany(p => p.Pages)
               .WithOne()
               .HasForeignKey(p => p.ProjectId);
    }
}
