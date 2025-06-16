using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Queries;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Metas.QueryHandlers;

public class MetaGetQueryHandler(
    IMapper mapper,
    IMetaService metaService,
    GetQueryValidator validationRules)
    : IQueryHandler<MetaGetQuery, ICollection<MetaDto>>
{
    public async Task<ICollection<MetaDto>> Handle(MetaGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.MetaFilter ?? new MetaFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

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
