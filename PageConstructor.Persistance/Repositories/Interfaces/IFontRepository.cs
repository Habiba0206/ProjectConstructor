using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IFontRepository
{
    IQueryable<Font> Get(
             Expression<Func<Font, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Font?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Font>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font> CreateAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font> UpdateAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font?> DeleteAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
