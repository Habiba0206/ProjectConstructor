using AutoMapper;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Queries;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Projects.QueryHandlers;

public class ProjectGetByIdQueryHandler(
    IMapper mapper,
    IProjectService projectService)
    : IQueryHandler<ProjectGetByIdQuery, ProjectDto>
{
    public async Task<ProjectDto> Handle(ProjectGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await projectService.GetByIdAsync(request.ProjectId, cancellationToken: cancellationToken);

        return mapper.Map<ProjectDto>(result);
    }
}
