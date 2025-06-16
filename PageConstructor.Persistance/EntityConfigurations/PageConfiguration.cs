using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Title).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Css).HasColumnType("text");
        
        builder.HasMany(p => p.Metas)
            .WithOne(m => m.Page)
            .HasForeignKey(m => m.PageId);

        builder.HasMany(p => p.Scripts)
            .WithOne(s => s.Page)
            .HasForeignKey(s => s.PageId);

        //builder.HasMany(p => p.Styles)
        //    .WithOne(s => s.Page)
        //    .HasForeignKey(s => s.PageId);

        builder.HasMany(p => p.Fonts)
            .WithOne(f => f.Page)
            .HasForeignKey(f => f.PageId);
    }
}

