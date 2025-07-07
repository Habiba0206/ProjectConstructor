using PageConstructor.Application.Components.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Components.Queries;

public class ComponentGetByIdQuery : IQuery<ComponentDto?>
{
    public Guid ComponentId { get; set; }
}
