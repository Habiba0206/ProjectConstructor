using PageConstructor.Application.Components.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Components.Queries;

public class ComponentGetQuery : IQuery<ICollection<ComponentDto>>
{
    public ComponentFilter ComponentFilter { get; set; }
}

