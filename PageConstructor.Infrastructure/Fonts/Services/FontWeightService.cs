using FluentValidation;
using System.Linq.Expressions;
using PageConstructor.Infrastructure.Fonts.Validators;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Enums;

namespace PageConstructor.Infrastructure.Fonts.Services;

public class FontWeightService(
    IFontWeightRepository fontWeightRepository,
    FontWeightValidator validator)
   : IFontWeightService
{
    public IQueryable<FontWeight> Get(
        Expression<Func<FontWeight, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    fontWeightRepository.Get(predicate, queryOptions);

    public IQueryable<FontWeight> Get(
        FontWeightFilter answerFilter,
        QueryOptions queryOptions = default) =>
    fontWeightRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(answerFilter);

    public ValueTask<FontWeight?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<FontWeight>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.CheckByIdAsync(id, cancellationToken);

    public ValueTask<FontWeight> CreateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.CreateAsync(fontWeight, commandOptions, cancellationToken);

    public async ValueTask<FontWeight> UpdateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return await fontWeightRepository.UpdateAsync(fontWeight, commandOptions, cancellationToken);
    }

    public ValueTask<FontWeight?> DeleteAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.DeleteAsync(fontWeight, commandOptions, cancellationToken);

    public ValueTask<FontWeight?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontWeightRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
