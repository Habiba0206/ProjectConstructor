using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Pages.Commands;

public class PagePatchCommand : ICommand<PagePatchDto>
{
    public PagePatchDto PagePatchDto { get; set; } = null!;
}
