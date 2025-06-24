using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Application.Projects.Commands;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Pages.Validators;
using PageConstructor.Infrastructure.Projects.Validators;

namespace PageConstructor.Infrastructure.Projects.CommandHandlers;

public class ProjectUpdateCommandHandler(
IMapper mapper,
IProjectService projectService,
    ProjectValidator validator) : ICommandHandler<ProjectUpdateCommand, ProjectDto>
{
    public async Task<ProjectDto> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.ProjectDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var project = mapper.Map<Project>(request.ProjectDto);

        var updatedProject = await projectService.UpdateAsync(project, cancellationToken: cancellationToken);

        return mapper.Map<ProjectDto>(updatedProject);
    }
}