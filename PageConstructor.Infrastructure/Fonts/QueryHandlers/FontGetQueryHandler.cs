using AutoMapper;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontGetQueryHandler(
    IMapper mapper,
    IFontService answerService)
    : IQueryHandler<FontGetQuery, ICollection<FontDto>>
{
    public async Task<ICollection<FontDto>> Handle(FontGetQuery request, CancellationToken cancellationToken)
    {
        var result = await answerService.Get(
            request.FontFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FontDto>>(result);
    }
}
