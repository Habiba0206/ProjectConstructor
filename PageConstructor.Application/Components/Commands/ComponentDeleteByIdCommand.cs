using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Components.Commands;

public class ComponentDeleteByIdCommand : ICommand<bool>
{
    public Guid ComponentId { get; set; }
}
