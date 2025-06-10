using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

internal class FontConfiguration : IEntityTypeConfiguration<Font>
{
    public void Configure(EntityTypeBuilder<Font> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name).IsRequired();
        builder.Property(f => f.Src).IsRequired();
        builder.Property(f => f.Display).IsRequired();

        builder.HasMany(f => f.Weights)
            .WithOne(w => w.Font)
            .HasForeignKey(w => w.FontId);
    }
}
