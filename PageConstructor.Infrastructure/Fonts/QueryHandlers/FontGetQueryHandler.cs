using AutoMapper;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using FluentValidation;
using PageConstructor.Application.Pages.Models;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontGetQueryHandler(
    IMapper mapper,
    IFontService fontService,
    GetQueryValidator validationRules)
    : IQueryHandler<FontGetQuery, ICollection<FontDto>>
{
    public async Task<ICollection<FontDto>> Handle(FontGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.FontFilter ?? new FontFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await fontService.Get(
            request.FontFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FontDto>>(result);
    }
}
