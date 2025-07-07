using AutoMapper;
using PageConstructor.Application.Components.Commands;
using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Components.CommandHandlers;

public class ComponentPatchCommandHandler(
    IComponentService componentService,
    IMapper mapper)
    : ICommandHandler<ComponentPatchCommand, ComponentPatchDto>
{
    public async Task<ComponentPatchDto> Handle(ComponentPatchCommand request, CancellationToken cancellationToken)
    {
        var component = await componentService.PatchAsync(request.ComponentPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<ComponentPatchDto>(component);
    }
}
