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
        var existingPage = await pageRepository.GetByIdAsync(page.Id) ?? throw new ArgumentNullException("This book doesn't exist");

        existingPage.ProjectId = page.ProjectId;
        existingPage.Title = page.Title;
        existingPage.Css = page.Css; 
        existingPage.IsPublished = page.IsPublished;

        return await pageRepository.UpdateAsync(existingPage, commandOptions, cancellationToken);
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
