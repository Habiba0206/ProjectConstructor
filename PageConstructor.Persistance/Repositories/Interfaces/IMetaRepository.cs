using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IMetaRepository
{
    IQueryable<Meta> Get(
             Expression<Func<Meta, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Meta?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Meta>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Meta> CreateAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Meta> UpdateAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Meta?> DeleteAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Meta?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
