using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Application.Fonts.Models;
using System.Linq.Expressions;

namespace PageConstructor.Application.Fonts.Services;

public interface IFontService
{
    IQueryable<Font> Get(
             Expression<Func<Font, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    IQueryable<Font> Get(
        FontFilter answerFilter,
        QueryOptions queryOptions = default);

    ValueTask<Font?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Font>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font> CreateAsync(
        Font book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font> UpdateAsync(
        Font book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font> PatchAsync(
        FontPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font?> DeleteAsync(
        Font book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Font?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
