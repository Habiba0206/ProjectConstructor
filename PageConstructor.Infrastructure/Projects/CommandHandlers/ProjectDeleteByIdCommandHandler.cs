using PageConstructor.Application.Projects.Commands;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Projects.CommandHandlers;

public class ProjectDeleteByIdCommandHandler(
    IProjectService projectService)
    : ICommandHandler<ProjectDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(ProjectDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await projectService.DeleteByIdAsync(request.ProjectId, cancellationToken: cancellationToken);

        return result is not null;
    }
}
