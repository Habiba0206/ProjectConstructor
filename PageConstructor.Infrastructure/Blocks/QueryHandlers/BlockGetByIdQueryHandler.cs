using AutoMapper;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Queries;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Blocks.QueryHandlers;

public class BlockGetByIdQueryHandler(
    IMapper mapper,
    IBlockService blockService)
    : IQueryHandler<BlockGetByIdQuery, BlockDto>
{
    public async Task<BlockDto> Handle(BlockGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await blockService.GetByIdAsync(request.BlockId, cancellationToken: cancellationToken);

        return mapper.Map<BlockDto>(result);
    }
}
