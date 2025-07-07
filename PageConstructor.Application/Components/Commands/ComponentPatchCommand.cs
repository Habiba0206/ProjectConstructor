using PageConstructor.Application.Components.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Components.Commands;

public class ComponentPatchCommand : ICommand<ComponentPatchDto>
{
    public ComponentPatchDto ComponentPatchDto { get; set; } = null!;
}
