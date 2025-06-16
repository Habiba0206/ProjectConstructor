using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Scripts.Validators;

namespace PageConstructor.Infrastructure.Scripts.CommandHandlers;

public class ScriptCreateCommandHandler(
    IMapper mapper,
    IScriptService scriptService,
    ScriptValidator validator) : ICommandHandler<ScriptCreateCommand, ScriptDto>
{
    public async Task<ScriptDto> Handle(ScriptCreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.ScriptDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var script = mapper.Map<Script>(request.ScriptDto);

        var createdScript = await scriptService.CreateAsync(script, cancellationToken: cancellationToken);

        return mapper.Map<ScriptDto>(createdScript);
    }
}
