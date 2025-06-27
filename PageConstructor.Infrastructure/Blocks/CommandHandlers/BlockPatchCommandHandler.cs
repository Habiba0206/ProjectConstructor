using AutoMapper;
using PageConstructor.Application.Blocks.Commands;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Blocks.CommandHandlers;

public class BlockPatchCommandHandler(
    IBlockService blockService,
    IMapper mapper)
    : ICommandHandler<BlockPatchCommand, BlockPatchDto>
{
    public async Task<BlockPatchDto> Handle(BlockPatchCommand request, CancellationToken cancellationToken)
    {
        var block = await blockService.PatchAsync(request.BlockPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<BlockPatchDto>(block);
    }
}