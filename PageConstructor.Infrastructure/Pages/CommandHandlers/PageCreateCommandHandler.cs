using AutoMapper;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Pages.CommandHandlers;

public class PageCreateCommandHandler(
    IMapper mapper,
    IPageService pageService) : ICommandHandler<PageCreateCommand, PageDto>
{
    public async Task<PageDto> Handle(PageCreateCommand request, CancellationToken cancellationToken)
    {
        var page = mapper.Map<Page>(request.PageDto);

        var createdPage = await pageService.CreateAsync(page, cancellationToken: cancellationToken);

        return mapper.Map<PageDto>(createdPage);
    }
}
