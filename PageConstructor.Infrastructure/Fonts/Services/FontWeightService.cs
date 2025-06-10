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
    IFontWeightRepository bookRepository,
    FontWeightValidator validator)
   : IFontWeightService
{
    public IQueryable<FontWeight> Get(
        Expression<Func<FontWeight, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    bookRepository.Get(predicate, queryOptions);

    public IQueryable<FontWeight> Get(
        FontWeightFilter answerFilter,
        QueryOptions queryOptions = default) =>
    bookRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(answerFilter);

    public ValueTask<FontWeight?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<FontWeight>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    bookRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<FontWeight> CreateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            fontWeight,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await bookRepository.CreateAsync(fontWeight, commandOptions, cancellationToken);
    }

    public async ValueTask<FontWeight> UpdateAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            fontWeight,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await bookRepository.UpdateAsync(fontWeight, commandOptions, cancellationToken);
    }

    public ValueTask<FontWeight?> DeleteAsync(
        FontWeight fontWeight,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.DeleteAsync(fontWeight, commandOptions, cancellationToken);

    public ValueTask<FontWeight?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
