using PageConstructor.Application.Components.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Components.Commands;

public class ComponentCreateCommand : ICommand<ComponentDto>
{
    public ComponentDto ComponentDto { get; set; }
}
