using AutoMapper;
using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Scripts.CommandHandlers;

public class ScriptCreateCommandHandler(
    IMapper mapper,
    IScriptService scriptService) : ICommandHandler<ScriptCreateCommand, ScriptDto>
{
    public async Task<ScriptDto> Handle(ScriptCreateCommand request, CancellationToken cancellationToken)
    {
        var script = mapper.Map<Script>(request.ScriptDto);

        var createdScript = await scriptService.CreateAsync(script, cancellationToken: cancellationToken);

        return mapper.Map<ScriptDto>(createdScript);
    }
}
