using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Blocks.Commands;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Blocks.Validators;

namespace PageConstructor.Infrastructure.Blocks.CommandHandlers;

public class BlockCreateCommandHandler(
    IMapper mapper,
    IBlockService blockService,
    BlockValidator validator) : ICommandHandler<BlockCreateCommand, BlockDto>
{
    public async Task<BlockDto> Handle(BlockCreateCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(
            request.BlockDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
                var block = mapper.Map<Block>(request.BlockDto);

        var createdBlock = await blockService.CreateAsync(block, cancellationToken: cancellationToken);

        return mapper.Map<BlockDto>(createdBlock);
    }
}
