using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IProjectRepository
{
    IQueryable<Project> Get(
             Expression<Func<Project, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Project?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Project>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Project> CreateAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Project> UpdateAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Project?> DeleteAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Project?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
