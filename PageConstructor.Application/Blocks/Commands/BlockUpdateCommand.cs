using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Blocks.Commands;

public class BlockUpdateCommand : ICommand<BlockDto>
{
    public BlockDto BlockDto { get; set; }
}
