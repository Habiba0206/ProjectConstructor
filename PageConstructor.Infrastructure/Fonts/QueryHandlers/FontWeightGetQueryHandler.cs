using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontWeightGetQueryHandler(
    IMapper mapper,
    IFontWeightService fontWeightService,
    GetQueryValidator validationRules)
    : IQueryHandler<FontWeightGetQuery, ICollection<FontWeightDto>>
{
    public async Task<ICollection<FontWeightDto>> Handle(FontWeightGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.FontWeightFilter ?? new FontWeightFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await fontWeightService.Get(
            request.FontWeightFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FontWeightDto>>(result);
    }
}
