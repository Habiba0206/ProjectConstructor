using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories;

public class ProjectRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Project, AppDbContext>(appDbContext, cacheBroker),
    IProjectRepository

{
    public IQueryable<Project> Get(
        Expression<Func<Project, bool>>? predicate = null,
        QueryOptions queryOptions = default)
    {
        var projects = base.Get(predicate, queryOptions)
            .Include(p => p.Pages).Where(p => !p.IsDeleted);

        return projects;
    }

    public ValueTask<Project?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Project>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Project> CreateAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(project, commandOptions, cancellationToken);

    public ValueTask<Project> UpdateAsync(
        Project project,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(project, commandOptions, cancellationToken);

    public ValueTask<Project?> DeleteAsync(
        Project project,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(project, commandOptions, cancellationToken);

    public ValueTask<Project?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
