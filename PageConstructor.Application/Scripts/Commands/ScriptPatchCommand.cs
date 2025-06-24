using PageConstructor.Application.Scripts.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Scripts.Commands;

public class ScriptPatchCommand : ICommand<ScriptPatchDto>
{
    public ScriptPatchDto ScriptPatchDto { get; set; }
}
