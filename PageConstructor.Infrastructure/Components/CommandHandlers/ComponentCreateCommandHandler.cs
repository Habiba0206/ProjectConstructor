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

public class ComponentCreateCommandHandler(
    IMapper mapper,
    IComponentService componentService,
    ComponentValidator validator) : ICommandHandler<ComponentCreateCommand, ComponentDto>
{
    public async Task<ComponentDto> Handle(ComponentCreateCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(
            request.ComponentDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var component = mapper.Map<Component>(request.ComponentDto);

        var createdComponent = await componentService.CreateAsync(component, cancellationToken: cancellationToken);

        return mapper.Map<ComponentDto>(createdComponent);
    }
}

