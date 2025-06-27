using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.Id);

        builder
            .Property(b => b.Name)
            .IsRequired();

        builder
            .Property(b => b.Content)
            .IsRequired()
            .HasColumnType("text");

        builder
            .Property(b => b.Css)
            .HasColumnType("text");

        builder
            .Property(b => b.Script)
            .HasColumnType("text");

        builder
            .Property(b => b.IsActive)
            .HasDefaultValue(true);
    }
}
