using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;
using PageConstructor.Infrastructure.Fonts.Validators;
using PageConstructor.Domain.Common.Exceptions;

namespace PageConstructor.Infrastructure.Fonts.Services;

public class FontService(
    IFontRepository fontRepository,
    FontValidator validator)
   : IFontService
{
    public IQueryable<Font> Get(
        Expression<Func<Font, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    fontRepository.Get(predicate, queryOptions);

    public IQueryable<Font> Get(
        FontFilter fontFilter,
        QueryOptions queryOptions = default) =>
    fontRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(fontFilter);

    public ValueTask<Font?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    fontRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Font>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    fontRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    fontRepository.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Font> CreateAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontRepository.CreateAsync(font, commandOptions, cancellationToken);

    public async ValueTask<Font> UpdateAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingFont = await fontRepository.GetByIdAsync(font.Id) ?? throw new NotFoundException(typeof(Font).Name, font.Id);

        existingFont.Name = font.Name;
        existingFont.Src = font.Src;
        existingFont.Display = font.Display;
        existingFont.PageId = font.PageId;

        return await fontRepository.UpdateAsync(existingFont, commandOptions, cancellationToken);
    }

    public async ValueTask<Font> PatchAsync(
        FontPatchDto patchDto,
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        var existing = await fontRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Font).Name, patchDto.Id);

        if (patchDto.Name is not null) existing.Name = patchDto.Name;
        if (patchDto.Src is not null) existing.Src = patchDto.Src;
        if (patchDto.Display is not null) existing.Display = patchDto.Display;
        if (patchDto.PageId.HasValue) existing.PageId = patchDto.PageId.Value;

        return await fontRepository.UpdateAsync(existing, commandOptions, cancellationToken);
    }

    public ValueTask<Font?> DeleteAsync(
        Font font,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontRepository.DeleteAsync(font, commandOptions, cancellationToken);

    public ValueTask<Font?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    fontRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
