using AutoMapper;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Queries;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Pages.QueryHandlers;

public class PageGetByProjectIdQueryHandler(
    IMapper mapper,
    IPageService pageService)
    : IQueryHandler<PageGetByProjectIdQuery, ICollection<PageDto>>
{
    public async Task<ICollection<PageDto>> Handle(PageGetByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var result = pageService.Get(page => page.ProjectId == request.ProjectId);

        return mapper.Map<ICollection<PageDto>>(result);
    }
}
