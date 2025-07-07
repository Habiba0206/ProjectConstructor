using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Components.Commands;
using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Components.Validators;

namespace PageConstructor.Infrastructure.Components.CommandHandlers;

public class ComponentUpdateCommandHandler(
    IMapper mapper,
    IComponentService componentService,
    ComponentValidator validator) : ICommandHandler<ComponentUpdateCommand, ComponentDto>
{
    public async Task<ComponentDto> Handle(ComponentUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.ComponentDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var component = mapper.Map<Component>(request.ComponentDto);

        var updatedBlock = await componentService.UpdateAsync(component, cancellationToken: cancellationToken);

        return mapper.Map<ComponentDto>(updatedBlock);
    }
}
