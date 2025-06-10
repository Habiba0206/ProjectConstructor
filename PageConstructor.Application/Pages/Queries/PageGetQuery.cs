using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Pages.Queries;

public class PageGetQuery : IQuery<ICollection<PageDto>>
{
    public PageFilter PageFilter { get; set; }
}
