using PageConstructor.Domain.Common.Commands;
using PageConstructor.Application.Scripts.Models;

namespace PageConstructor.Application.Scripts.Commands;

public record ScriptCreateCommand : ICommand<ScriptDto>
{
    public ScriptDto ScriptDto { get; set; }
}
