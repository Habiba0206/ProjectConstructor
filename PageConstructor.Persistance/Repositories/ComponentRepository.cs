using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories;

public class ComponentRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Component, AppDbContext>(appDbContext, cacheBroker),
    IComponentRepository

{
    public IQueryable<Component> Get(
        Expression<Func<Component, bool>>? predicate = null,
        QueryOptions queryOptions = default)
    {
        var components = base.Get(predicate, queryOptions);

        return components;
    }

    public ValueTask<Component?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Component>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Component> CreateAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(component, commandOptions, cancellationToken);

    public ValueTask<Component> UpdateAsync(
        Component component,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(component, commandOptions, cancellationToken);

    public ValueTask<Component?> DeleteAsync(
        Component component,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(component, commandOptions, cancellationToken);

    public ValueTask<Component?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}

