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

public class ScriptUpdateCommandHandler(
    IMapper mapper,
    IScriptService scriptService,
    ScriptValidator validator) : ICommandHandler<ScriptUpdateCommand, ScriptDto>
{
    public async Task<ScriptDto> Handle(ScriptUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.ScriptDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var script = mapper.Map<Script>(request.ScriptDto);

        var updatedScript = await scriptService.UpdateAsync(script, cancellationToken: cancellationToken);

        return mapper.Map<ScriptDto>(updatedScript);
    }
}
