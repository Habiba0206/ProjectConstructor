using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Blocks.Commands;

public class BlockDeleteByIdCommand : ICommand<bool>
{
    public Guid BlockId { get; set; }
}
