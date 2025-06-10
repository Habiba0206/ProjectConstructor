using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.EntityConfigurations;

internal class FontWeightConfiguration : IEntityTypeConfiguration<FontWeight>
{
    public void Configure(EntityTypeBuilder<FontWeight> builder)
    {
        builder.HasKey(w => w.Id);
    }
}
