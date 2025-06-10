using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Fonts.Models;

namespace PageConstructor.Application.Fonts.Queries;

public class FontGetQuery : IQuery<ICollection<FontDto>>
{
    public FontFilter FontFilter { get; set; }
}
