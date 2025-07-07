using FluentValidation;
using System.Linq.Expressions;
using PageConstructor.Infrastructure.Pages.Validators;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Enums;
using PageConstructor.Domain.Common.Exceptions;
using System.Dynamic;

namespace PageConstructor.Infrastructure.Pages.Services;

public class PageService(
    IPageRepository pageRepository)
   : IPageService
{
    public IQueryable<Page> Get(
        Expression<Func<Page, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    pageRepository.Get(predicate, queryOptions);

    public IQueryable<Page> Get(
        PageFilter pageFilter,
        QueryOptions queryOptions = default) =>
    pageRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(pageFilter);

    public ValueTask<Page?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    pageRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Page>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    pageRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    pageRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<Page> CreateAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    await pageRepository.CreateAsync(page, commandOptions, cancellationToken);

    public async ValueTask<Page> UpdateAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingPage = await pageRepository.GetByIdAsync(page.Id) ?? throw new NotFoundException(typeof(Page).Name, page.Id);

        existingPage.ProjectId = page.ProjectId;
        existingPage.Title = page.Title;
        existingPage.UrlPath = page.UrlPath;
        existingPage.Html = page.Html;
        existingPage.Css = page.Css; 
        existingPage.IsPublished = page.IsPublished;
        existingPage.LastSaved = page.LastSaved;

        return await pageRepository.UpdateAsync(existingPage, commandOptions, cancellationToken);
    }

    public async ValueTask<Page> PatchAsync(
        PagePatchDto patchDto, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        var existing = await pageRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Page).Name, patchDto.Id);

        if (patchDto.ProjectId.HasValue) existing.ProjectId = patchDto.ProjectId.Value;
        if (patchDto.Title is not null) existing.Title = patchDto.Title;
        if (patchDto.UrlPath is not null) existing.UrlPath = patchDto.UrlPath;
        if (patchDto.Html is not null) existing.Html = patchDto.Html;
        if (patchDto.Css is not null) existing.Css = patchDto.Css;
        if (patchDto.IsPublished.HasValue) existing.IsPublished = patchDto.IsPublished.Value;
        if (patchDto.LastSaved.HasValue) existing.LastSaved = patchDto.LastSaved.Value;

        return await pageRepository.UpdateAsync(existing, cancellationToken: cancellationToken);
    }

    public ValueTask<Page?> DeleteAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    pageRepository.DeleteAsync(page, commandOptions, cancellationToken);

    public ValueTask<Page?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    pageRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}