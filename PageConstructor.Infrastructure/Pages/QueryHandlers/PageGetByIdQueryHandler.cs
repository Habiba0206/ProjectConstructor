using AutoMapper;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Queries;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Pages.QueryHandlers;

public class PageGetByIdQueryHandler(
    IMapper mapper,
    IPageService pageService)
    : IQueryHandler<PageGetByIdQuery, PageDto>
{
    public async Task<PageDto> Handle(PageGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await pageService.GetByIdAsync(request.PageId, cancellationToken: cancellationToken);

        return mapper.Map<PageDto>(result);
    }
}
