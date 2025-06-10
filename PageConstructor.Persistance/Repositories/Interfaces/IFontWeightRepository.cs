using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IFontWeightRepository
{
    IQueryable<FontWeight> Get(
             Expression<Func<FontWeight, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<FontWeight?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<FontWeight>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<FontWeight> CreateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<FontWeight> UpdateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<FontWeight?> DeleteAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<FontWeight?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
