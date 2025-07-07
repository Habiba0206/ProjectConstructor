using PageConstructor.Application.Components.Commands;
using PageConstructor.Application.Components.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Components.CommandHandlers;

public class ComponentDeleteByIdCommandHandler(
    IComponentService componentService)
    : ICommandHandler<ComponentDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(ComponentDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await componentService.DeleteByIdAsync(request.ComponentId, cancellationToken: cancellationToken);

        return result is not null;
    }
}
