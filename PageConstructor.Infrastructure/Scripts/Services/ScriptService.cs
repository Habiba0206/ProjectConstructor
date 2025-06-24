using System.Linq.Expressions;
using PageConstructor.Infrastructure.Scripts.Validators;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Exceptions;

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
        var existingScript = await scriptRepository.GetByIdAsync(script.Id) ?? throw new NotFoundException(typeof(Script).Name, script.Id);

        existingScript.Type = script.Type;
        existingScript.Src = script.Src;
        existingScript.Lang = script.Lang;
        existingScript.Modules = script.Modules;
        existingScript.Async = script.Async;
        existingScript.PageId = script.PageId;

        return await scriptRepository.UpdateAsync(existingScript, commandOptions, cancellationToken);
    }

    public async ValueTask<Script> PatchAsync(
        ScriptPatchDto patchDto, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
{
    var existing = await scriptRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                  ?? throw new NotFoundException(typeof(Script).Name, patchDto.Id);

    if (patchDto.Type is not null) existing.Type = patchDto.Type;
    if (patchDto.Src is not null) existing.Src = patchDto.Src;
    if (patchDto.Lang is not null) existing.Lang = patchDto.Lang;
    if (patchDto.Modules.HasValue) existing.Modules = patchDto.Modules.Value;
    if (patchDto.Async.HasValue) existing.Async = patchDto.Async.Value;
    if (patchDto.PageId.HasValue) existing.PageId = patchDto.PageId.Value;

    return await scriptRepository.UpdateAsync(existing, cancellationToken: cancellationToken);
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
