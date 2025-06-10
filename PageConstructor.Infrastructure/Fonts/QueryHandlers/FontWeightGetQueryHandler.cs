using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontWeightGetQueryHandler(
    IMapper mapper,
    IFontWeightService fontWeightService)
    : IQueryHandler<FontWeightGetQuery, ICollection<FontWeightDto>>
{
    public async Task<ICollection<FontWeightDto>> Handle(FontWeightGetQuery request, CancellationToken cancellationToken)
    {
        var result = await fontWeightService.Get(
            request.FontWeightFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FontWeightDto>>(result);
    }
}
