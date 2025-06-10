using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IScriptRepository
{
    IQueryable<Script> Get(
             Expression<Func<Script, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Script> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Script>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Script> CreateAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Script> UpdateAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Script?> DeleteAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Script?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
