using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Queries;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Pages.QueryHandlers;

public class PageGetQueryHandler(
    IMapper mapper,
    IPageService pageService)
    : IQueryHandler<PageGetQuery, ICollection<PageDto>>
{
    public async Task<ICollection<PageDto>> Handle(PageGetQuery request, CancellationToken cancellationToken)
    {
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
