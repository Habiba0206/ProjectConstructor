using AutoMapper;
using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Queries;
using PageConstructor.Application.Components.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Components.QueryHandlers;

public class ComponentGetByIdQueryHandler(
    IMapper mapper,
    IComponentService componentService)
    : IQueryHandler<ComponentGetByIdQuery, ComponentDto>
{
    public async Task<ComponentDto> Handle(ComponentGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await componentService.GetByIdAsync(request.ComponentId, cancellationToken: cancellationToken);

        return mapper.Map<ComponentDto>(result);
    }
}
