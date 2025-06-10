using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Queries;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Metas.QueryHandlers;

public class MetaGetQueryHandler(
    IMapper mapper,
    IMetaService metaService)
    : IQueryHandler<MetaGetQuery, ICollection<MetaDto>>
{
    public async Task<ICollection<MetaDto>> Handle(MetaGetQuery request, CancellationToken cancellationToken)
    {
        var result = await metaService.Get(
            request.MetaFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<MetaDto>>(result);
    }
}
