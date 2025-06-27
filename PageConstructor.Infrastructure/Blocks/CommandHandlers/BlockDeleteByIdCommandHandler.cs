using PageConstructor.Application.Blocks.Commands;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Blocks.CommandHandlers;

public class BlockDeleteByIdCommandHandler(
    IProjectService blockService)
    : ICommandHandler<BlockDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(BlockDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await blockService.DeleteByIdAsync(request.BlockId, cancellationToken: cancellationToken);

        return result is not null;
    }
}
