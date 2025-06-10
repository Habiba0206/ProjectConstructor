using AutoMapper;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Queries;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Metas.QueryHandlers;

public class MetaGetByIdQueryHandler(
    IMapper mapper,
    IMetaService metaService)
    : IQueryHandler<MetaGetByIdQuery, MetaDto>
{
    public async Task<MetaDto> Handle(MetaGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await metaService.GetByIdAsync(request.MetaId, cancellationToken: cancellationToken);

        return mapper.Map<MetaDto>(result);
    }
}
