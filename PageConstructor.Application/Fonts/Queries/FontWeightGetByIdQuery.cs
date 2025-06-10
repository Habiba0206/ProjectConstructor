using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Fonts.Queries;

public class FontWeightGetByIdQuery : IQuery<FontWeightDto?>
{
    public Guid FontWeightId { get; set; }
}
