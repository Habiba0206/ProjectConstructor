using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Fonts.Queries;

public class FontWeightGetQuery : IQuery<ICollection<FontWeightDto>>
{
    public FontWeightFilter FontWeightFilter { get; set; }
}
