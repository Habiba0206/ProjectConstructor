using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories;

public class BlockRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Block, AppDbContext>(appDbContext, cacheBroker),
    IBlockRepository

{
    public IQueryable<Block> Get(
        Expression<Func<Block, bool>>? predicate = null,
        QueryOptions queryOptions = default)
    {
        var blocks = base.Get(predicate, queryOptions);

        return blocks;
    }

    public ValueTask<Block?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Block>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Block> CreateAsync(
        Block block,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(block, commandOptions, cancellationToken);

    public ValueTask<Block> UpdateAsync(
        Block block,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(block, commandOptions, cancellationToken);

    public ValueTask<Block?> DeleteAsync(
        Block block,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(block, commandOptions, cancellationToken);

    public ValueTask<Block?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
