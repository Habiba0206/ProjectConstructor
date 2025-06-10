using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Scripts.Commands;

public class ScriptDeleteByIdCommand : ICommand<bool>
{
    public Guid ScriptId { get; set; }
}
