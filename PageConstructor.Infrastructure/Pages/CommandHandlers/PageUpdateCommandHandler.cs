using AutoMapper;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Pages.CommandHandlers;

public class PageUpdateCommandHandler(
    IMapper mapper,
    IPageService pageService) : ICommandHandler<PageUpdateCommand, PageDto>
{
    public async Task<PageDto> Handle(PageUpdateCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Page>(request.PageDto);

        var updatedPage = await pageService.UpdateAsync(book, cancellationToken: cancellationToken);

        return mapper.Map<PageDto>(updatedPage);
    }
}
