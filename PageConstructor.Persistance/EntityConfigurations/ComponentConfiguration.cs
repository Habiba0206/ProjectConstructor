using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.HtmlContent)
            .IsRequired();

        builder.Property(c => c.Css)
            .IsRequired(false);

        builder.Property(c => c.PreviewImageUrl)
            .HasMaxLength(255);

        builder.HasOne(c => c.Block)
            .WithMany(b => b.Components)
            .HasForeignKey(c => c.BlockId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}