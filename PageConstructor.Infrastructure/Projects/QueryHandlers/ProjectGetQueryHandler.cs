using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Queries;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Projects.QueryHandlers;

public class ProjectGetQueryHandler(
    IMapper mapper,
    IProjectService projectService,
    GetQueryValidator validationRules)
    : IQueryHandler<ProjectGetQuery, ICollection<ProjectDto>>
{
    public async Task<ICollection<ProjectDto>> Handle(ProjectGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.ProjectFilter ?? new ProjectFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await projectService.Get(
            request.ProjectFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<ProjectDto>>(result);
    }
}
