using System.Linq.Expressions;
using PageConstructor.Infrastructure.Metas.Validators;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Exceptions;

namespace PageConstructor.Infrastructure.Metas.Services;

public class MetaService(
    IMetaRepository metaRepository,
    MetaValidator validator)
   : IMetaService
{
    public IQueryable<Meta> Get(
        Expression<Func<Meta, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    metaRepository.Get(predicate, queryOptions);

    public IQueryable<Meta> Get(
        MetaFilter answerFilter,
        QueryOptions queryOptions = default) =>
    metaRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(answerFilter);

    public ValueTask<Meta?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    metaRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Meta>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    metaRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    metaRepository.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Meta> CreateAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    metaRepository.CreateAsync(meta, commandOptions, cancellationToken);

    public async ValueTask<Meta> UpdateAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingMeta = await metaRepository.GetByIdAsync(meta.Id) ?? throw new NotFoundException(typeof(Meta).Name, meta.Id);

        existingMeta.Title = meta.Title;
        existingMeta.Description = meta.Description;
        existingMeta.PageId = meta.PageId;
        existingMeta.OgDescription = meta.OgDescription;
        existingMeta.OgTitle = meta.OgTitle;

        return await metaRepository.UpdateAsync(existingMeta, commandOptions, cancellationToken);
    }

    public async ValueTask<Meta> PatchAsync(
        MetaPatchDto patchDto, 
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existing = await metaRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Meta).Name, patchDto.Id);

        if (patchDto.Title is not null) existing.Title = patchDto.Title;
        if (patchDto.Description is not null) existing.Description = patchDto.Description;
        if (patchDto.OgTitle is not null) existing.OgTitle = patchDto.OgTitle;
        if (patchDto.OgDescription is not null) existing.OgDescription = patchDto.OgDescription;
        if (patchDto.Keywords is not null) existing.Keywords = patchDto.Keywords;
        if (patchDto.PageId.HasValue) existing.PageId = patchDto.PageId.Value;

        return await metaRepository.UpdateAsync(existing, commandOptions, cancellationToken);
    }

    public ValueTask<Meta?> DeleteAsync(
        Meta meta,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    metaRepository.DeleteAsync(meta, commandOptions, cancellationToken);

    public ValueTask<Meta?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    metaRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
