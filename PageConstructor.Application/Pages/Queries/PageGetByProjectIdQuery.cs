using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Pages.Queries;

public class PageGetByProjectIdQuery : IQuery<ICollection<PageDto>>
{
    public Guid ProjectId { get; set; }
}