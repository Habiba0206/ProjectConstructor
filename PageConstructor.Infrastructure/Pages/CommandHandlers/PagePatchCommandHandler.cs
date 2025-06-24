using AutoMapper;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Pages.CommandHandlers;

public class PagePatchCommandHandler(
    IPageService pageService,
    IMapper mapper)
    : ICommandHandler<PagePatchCommand, PagePatchDto>
{
    public async Task<PagePatchDto> Handle(PagePatchCommand request, CancellationToken cancellationToken)
    {
        var page = await pageService.PatchAsync(request.PagePatchDto, cancellationToken: cancellationToken);

        return mapper.Map<PagePatchDto>(page);
    }
}