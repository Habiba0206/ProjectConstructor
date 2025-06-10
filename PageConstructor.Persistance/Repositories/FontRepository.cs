using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Threading;

namespace PageConstructor.Persistence.Repositories;

public class FontRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Font, AppDbContext>(appDbContext, cacheBroker),
    IFontRepository

{
    public IQueryable<Font> Get(
        Expression<Func<Font, bool>>? predicate = null,
        QueryOptions queryOptions = default)
    {
        var fonts = base
            .Get(predicate, queryOptions)
            .Include(f => f.Weights);

        return fonts;
    }

    public ValueTask<Font?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Font>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Font> CreateAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(font, commandOptions, cancellationToken);

    public ValueTask<Font> UpdateAsync(
        Font font,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(font, commandOptions, cancellationToken);

    public ValueTask<Font?> DeleteAsync(
        Font font,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(font, commandOptions, cancellationToken);

    public ValueTask<Font?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
