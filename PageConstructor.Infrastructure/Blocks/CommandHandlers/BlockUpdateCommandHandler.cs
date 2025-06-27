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

public class BlockUpdateCommandHandler(
    IMapper mapper,
    IBlockService projectService,
    BlockValidator validator) : ICommandHandler<BlockUpdateCommand, BlockDto>
{
    public async Task<BlockDto> Handle(BlockUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.BlockDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var project = mapper.Map<Block>(request.BlockDto);

        var updatedBlock = await projectService.UpdateAsync(project, cancellationToken: cancellationToken);

        return mapper.Map<BlockDto>(updatedBlock);
    }
}
