using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Blocks.Commands;

public class BlockPatchCommand : ICommand<BlockPatchDto>
{
    public BlockPatchDto BlockPatchDto { get; set; } = null!;
}
