using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Blocks.Queries;

public class BlockGetByIdQuery : IQuery<BlockDto?>
{
    public Guid BlockId { get; set; }
}
