using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Blocks.Commands;

public class BlockCreateCommand : ICommand<BlockDto>
{
    public BlockDto BlockDto { get; set; }
}
