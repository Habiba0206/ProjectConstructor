using AutoMapper;
using PageConstructor.Application.Projects.Commands;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Projects.CommandHandlers;

public class ProjectPatchCommandHandler(
    IProjectService projectService,
    IMapper mapper)
    : ICommandHandler<ProjectPatchCommand, ProjectPatchDto>
{
    public async Task<ProjectPatchDto> Handle(ProjectPatchCommand request, CancellationToken cancellationToken)
    {
        var project = await projectService.PatchAsync(request.ProjectPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<ProjectPatchDto>(project);
    }
}
