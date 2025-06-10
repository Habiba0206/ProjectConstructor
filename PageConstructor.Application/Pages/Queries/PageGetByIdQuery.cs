using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Pages.Queries;

public class PageGetByIdQuery : IQuery<PageDto?>
{
    public Guid PageId { get; set; }
}
