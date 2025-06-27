using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Blocks.Queries;

public class BlockGetQuery : IQuery<ICollection<BlockDto>>
{
    public BlockFilter BlockFilter { get; set; }
}
