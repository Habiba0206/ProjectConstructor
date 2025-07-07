using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Exceptions;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Infrastructure.Components.Services;

public class ComponentService(
    IComponentRepository componentRepository)
   : IComponentService
{
    public IQueryable<Component> Get(
        Expression<Func<Component, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    componentRepository.Get(predicate, queryOptions);

    public IQueryable<Component> Get(
        ComponentFilter componentFilter,
        QueryOptions queryOptions = default) =>
    componentRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(componentFilter);

    public ValueTask<Component?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    componentRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Component>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    componentRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    componentRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<Component> CreateAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    await componentRepository.CreateAsync(component, commandOptions, cancellationToken);

    public async ValueTask<Component> UpdateAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingComponent = await componentRepository.GetByIdAsync(component.Id) ?? throw new NotFoundException(typeof(Component).Name, component.Id);

        existingComponent.Title = component.Title;
        existingComponent.HtmlContent = component.HtmlContent;
        existingComponent.Css = component.Css;
        existingComponent.PreviewImageUrl = component.PreviewImageUrl;
        existingComponent.BlockId = component.BlockId;

        return await componentRepository.UpdateAsync(existingComponent, commandOptions, cancellationToken);
    }

    public async ValueTask<Component> PatchAsync(
        ComponentPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existing = await componentRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Component).Name, patchDto.Id);

        if (patchDto.Title is not null) existing.Title = patchDto.Title;
        if (patchDto.HtmlContent is not null) existing.HtmlContent = patchDto.HtmlContent;
        if (patchDto.Css is not null) existing.Css = patchDto.Css;
        if (patchDto.PreviewImageUrl is not null) existing.PreviewImageUrl = patchDto.PreviewImageUrl;
        if (patchDto.BlockId is not null) existing.BlockId = patchDto.BlockId.Value;

        return await componentRepository.UpdateAsync(existing, cancellationToken: cancellationToken);
    }

    public ValueTask<Component?> DeleteAsync(
        Component component,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    componentRepository.DeleteAsync(component, commandOptions, cancellationToken);

    public ValueTask<Component?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    componentRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}