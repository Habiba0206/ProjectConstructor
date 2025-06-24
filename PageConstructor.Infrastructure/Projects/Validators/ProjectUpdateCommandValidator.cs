using FluentValidation;
using PageConstructor.Application.Projects.Commands;

namespace PageConstructor.Infrastructure.Projects.Validators;

public class ProjectUpdateCommandValidator : AbstractValidator<ProjectUpdateCommand>
{
    public ProjectUpdateCommandValidator()
    {
        RuleFor(x => x.ProjectDto)
            .NotNull().WithMessage("ProjectDto cannot be null.")
            .SetValidator(new ProjectValidator());
    }
}