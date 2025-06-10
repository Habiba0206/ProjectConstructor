using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Pages.Commands;

public class PageDeleteByIdCommand : ICommand<bool>
{
    public Guid PageId { get; set; }
}
