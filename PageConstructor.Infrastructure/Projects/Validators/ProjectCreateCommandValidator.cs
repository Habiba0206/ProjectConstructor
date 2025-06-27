using FluentValidation;
using PageConstructor.Application.Projects.Commands;

namespace PageConstructor.Infrastructure.Projects.Validators;

public class ProjectCreateCommandValidator : AbstractValidator<ProjectCreateCommand>
{
    public ProjectCreateCommandValidator()
    {
        RuleFor(x => x.ProjectDto)
            .NotNull().WithMessage("ProjectDto cannot be null.")
            .SetValidator(new ProjectValidator());
    }
}

