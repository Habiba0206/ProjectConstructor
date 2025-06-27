using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Application.Blocks.Services;

public interface IBlockService
{
    IQueryable<Block> Get(
             Expression<Func<Block, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    IQueryable<Block> Get(
        BlockFilter blockFilter,
        QueryOptions queryOptions = default);

    ValueTask<Block?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Block>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Block> CreateAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Block> UpdateAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Block> PatchAsync(
        BlockPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Block?> DeleteAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Block?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
