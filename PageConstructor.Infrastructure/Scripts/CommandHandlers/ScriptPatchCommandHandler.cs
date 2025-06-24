using AutoMapper;
using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Scripts.CommandHandlers;

public class ScriptPatchCommandHandler(
    IScriptService scriptService,
    IMapper mapper)
    : ICommandHandler<ScriptPatchCommand, ScriptPatchDto>
{
    public async Task<ScriptPatchDto> Handle(ScriptPatchCommand request, CancellationToken cancellationToken)
    {
        var script = await scriptService.PatchAsync(request.ScriptPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<ScriptPatchDto>(script);
    }
}