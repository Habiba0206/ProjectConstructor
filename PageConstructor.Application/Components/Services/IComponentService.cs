using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;
using PageConstructor.Application.Components.Models;

namespace PageConstructor.Application.Components.Services;

public interface IComponentService
{
    IQueryable<Component> Get(
             Expression<Func<Component, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    IQueryable<Component> Get(
        ComponentFilter componentFilter,
        QueryOptions queryOptions = default);

    ValueTask<Component?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Component>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Component> CreateAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Component> UpdateAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Component> PatchAsync(
        ComponentPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Component?> DeleteAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Component?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
