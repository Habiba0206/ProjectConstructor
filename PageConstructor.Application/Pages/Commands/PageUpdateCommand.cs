using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Pages.Commands;

public class PageUpdateCommand : ICommand<PageDto>
{
    public PageDto PageDto { get; set; }
}
