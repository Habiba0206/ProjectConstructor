using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

public class ScriptConfiguration : IEntityTypeConfiguration<Script>
{
    public void Configure(EntityTypeBuilder<Script> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Type).IsRequired();
        builder.Property(s => s.Src).IsRequired();
        builder.Property(s => s.Lang).IsRequired().HasDefaultValue("css");
        builder.Property(s => s.Modules).HasDefaultValue(false);
        builder.Property(s => s.Async).HasDefaultValue(false);
    }
}
