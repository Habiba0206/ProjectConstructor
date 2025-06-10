using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Scripts.CommandHandlers;

public class ScriptDeleteByIdCommandHandler(
    IScriptService scriptService)
    : ICommandHandler<ScriptDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(ScriptDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await scriptService.DeleteByIdAsync(request.ScriptId, cancellationToken: cancellationToken);

        return result is not null;
    }
}