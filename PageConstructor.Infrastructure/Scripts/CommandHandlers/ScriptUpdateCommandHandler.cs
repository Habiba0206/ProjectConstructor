using AutoMapper;
using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Scripts.CommandHandlers;

public class ScriptUpdateCommandHandler(
    IMapper mapper,
    IScriptService scriptService) : ICommandHandler<ScriptUpdateCommand, ScriptDto>
{
    public async Task<ScriptDto> Handle(ScriptUpdateCommand request, CancellationToken cancellationToken)
    {
        var script = mapper.Map<Script>(request.ScriptDto);

        var updatedScript = await scriptService.UpdateAsync(script, cancellationToken: cancellationToken);

        return mapper.Map<ScriptDto>(updatedScript);
    }
}
