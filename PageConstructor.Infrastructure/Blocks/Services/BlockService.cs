using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Exceptions;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Infrastructure.Blocks.Services;

public class BlockService(
    IBlockRepository blockRepository)
   : IBlockService
{
    public IQueryable<Block> Get(
        Expression<Func<Block, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    blockRepository.Get(predicate, queryOptions);

    public IQueryable<Block> Get(
        BlockFilter blockFilter,
        QueryOptions queryOptions = default) =>
    blockRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(blockFilter);

    public ValueTask<Block?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    blockRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Block>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    blockRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    blockRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<Block> CreateAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    await blockRepository.CreateAsync(block, commandOptions, cancellationToken);

    public async ValueTask<Block> UpdateAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingBlock = await blockRepository.GetByIdAsync(block.Id) ?? throw new NotFoundException(typeof(Block).Name, block.Id);

        existingBlock.Name = block.Name;
        existingBlock.Category = block.Category;
        existingBlock.Label = block.Label;
        existingBlock.Content = block.Content;
        existingBlock.Css = block.Css;
        existingBlock.Script = block.Script;
        existingBlock.PreviewImageUrl = block.PreviewImageUrl;
        existingBlock.IsActive = block.IsActive;

        return await blockRepository.UpdateAsync(existingBlock, commandOptions, cancellationToken);
    }

    public async ValueTask<Block> PatchAsync(
        BlockPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existing = await blockRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Block).Name, patchDto.Id);

        if (patchDto.Name is not null) existing.Name = patchDto.Name;
        if (patchDto.Category is not null) existing.Category = patchDto.Category;
        if (patchDto.Label is not null) existing.Label = patchDto.Label;
        if (patchDto.Content is not null) existing.Content = patchDto.Content;
        if (patchDto.Css is not null) existing.Css = patchDto.Css;
        if (patchDto.Script is not null) existing.Script = patchDto.Script;
        if (patchDto.PreviewImageUrl is not null) existing.PreviewImageUrl = patchDto.PreviewImageUrl;
        if (patchDto.IsActive.HasValue) existing.IsActive = patchDto.IsActive.Value;

        return await blockRepository.UpdateAsync(existing, cancellationToken: cancellationToken);
    }

    public ValueTask<Block?> DeleteAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    blockRepository.DeleteAsync(block, commandOptions, cancellationToken);

    public ValueTask<Block?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    blockRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}