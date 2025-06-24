using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Pages.CommandHandlers;

public class PageDeleteByIdCommandHandler(
    IPageService pageService)
    : ICommandHandler<PageDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(PageDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await pageService.DeleteByIdAsync(request.PageId, cancellationToken: cancellationToken);

        return result is not null;
    }
}