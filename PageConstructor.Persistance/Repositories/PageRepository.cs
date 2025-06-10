using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories;

public class PageRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Page, AppDbContext>(appDbContext, cacheBroker),
    IPageRepository

{
    public IQueryable<Page> Get(
        Expression<Func<Page, bool>>? predicate = null,
        QueryOptions queryOptions = default)
    {
        var pages = base.Get(predicate, queryOptions)
            .Include(p => p.Metas).Where(m => !m.IsDeleted)
            .Include(p => p.Scripts).Where(s => !s.IsDeleted)
            .Include(p => p.Fonts).Where(f => !f.IsDeleted);

        return pages;
    }

    public ValueTask<Page?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Page>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Page> CreateAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(page, commandOptions, cancellationToken);

    public ValueTask<Page> UpdateAsync(
        Page page,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(page, commandOptions, cancellationToken);

    public ValueTask<Page?> DeleteAsync(
        Page page,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(page, commandOptions, cancellationToken);

    public ValueTask<Page?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
