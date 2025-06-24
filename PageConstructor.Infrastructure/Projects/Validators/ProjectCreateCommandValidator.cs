using FluentValidation;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Projects.Commands;
using PageConstructor.Infrastructure.Pages.Validators;

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

