using FluentValidation;
using System.Linq.Expressions;
using PageConstructor.Infrastructure.Scripts.Validators;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Enums;

namespace PageConstructor.Infrastructure.Scripts.Services;

public class ScriptService(
    IScriptRepository scriptRepository,
    ScriptValidator validator)
   : IScriptService
{
    public IQueryable<Script> Get(
        Expression<Func<Script, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    scriptRepository.Get(predicate, queryOptions);

    public IQueryable<Script> Get(
        ScriptFilter scriptFilter,
        QueryOptions queryOptions = default) =>
    scriptRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(scriptFilter);

    public ValueTask<Script?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    scriptRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Script>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    scriptRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    scriptRepository.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Script> CreateAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    scriptRepository.CreateAsync(script, commandOptions, cancellationToken);

    public async ValueTask<Script> UpdateAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingScript = await scriptRepository.GetByIdAsync(script.Id) ?? throw new ArgumentNullException("This book doesn't exist");

        existingScript.Type = script.Type;
        existingScript.Src = script.Src;
        existingScript.Lang = script.Lang;
        existingScript.Modules = script.Modules;
        existingScript.Async = script.Async;
        existingScript.PageId = script.PageId;

        return await scriptRepository.UpdateAsync(existingScript, commandOptions, cancellationToken);
    }

    public ValueTask<Script?> DeleteAsync(
        Script script,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    scriptRepository.DeleteAsync(script, commandOptions, cancellationToken);

    public ValueTask<Script?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    scriptRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
