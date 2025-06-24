using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Projects.Commands;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Projects.Validators;

namespace PageConstructor.Infrastructure.Projects.CommandHandlers;

public class ProjectCreateCommandHandler(
    IMapper mapper,
    IProjectService projectService,
    ProjectValidator validator) : ICommandHandler<ProjectCreateCommand, ProjectDto>
{
    public async Task<ProjectDto> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(
            request.ProjectDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var project = mapper.Map<Project>(request.ProjectDto);

        var createdProject = await projectService.CreateAsync(project, cancellationToken: cancellationToken);

        return mapper.Map<ProjectDto>(createdProject);
    }
}
