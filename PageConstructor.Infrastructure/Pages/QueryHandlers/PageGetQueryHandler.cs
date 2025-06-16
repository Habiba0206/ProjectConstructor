using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Queries;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Pages.QueryHandlers;

public class PageGetQueryHandler(
    IMapper mapper,
    IPageService pageService,
    GetQueryValidator validationRules)
    : IQueryHandler<PageGetQuery, ICollection<PageDto>>
{
    public async Task<ICollection<PageDto>> Handle(PageGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.PageFilter ?? new PageFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await pageService.Get(
            request.PageFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<PageDto>>(result);
    }
}
