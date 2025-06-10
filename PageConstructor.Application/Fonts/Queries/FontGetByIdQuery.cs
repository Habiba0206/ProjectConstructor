using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Fonts.Models;

namespace PageConstructor.Application.Fonts.Queries;

public class FontGetByIdQuery : IQuery<FontDto?>
{
    public Guid FontId { get; set; }
}
