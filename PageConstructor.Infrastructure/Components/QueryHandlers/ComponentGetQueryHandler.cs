using AutoMapper;
using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Queries;
using PageConstructor.Application.Components.Services;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Domain.Common.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace PageConstructor.Infrastructure.Components.QueryHandlers;

public class ComponentGetQueryHandler(
    IMapper mapper,
    IComponentService componentService,
    GetQueryValidator validationRules)
    : IQueryHandler<ComponentGetQuery, ICollection<ComponentDto>>
{
    public async Task<ICollection<ComponentDto>> Handle(ComponentGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.ComponentFilter ?? new ComponentFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await componentService.Get(
            request.ComponentFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<ComponentDto>>(result);
    }
}
