﻿using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using System.Linq.Expressions;

namespace PageConstructor.Persistence.Repositories.Interfaces;

public interface IPageRepository
{
    IQueryable<Page> Get(
             Expression<Func<Page, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Page?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Page>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Page> CreateAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Page> UpdateAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Page?> DeleteAsync(
        Page page,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Page?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
