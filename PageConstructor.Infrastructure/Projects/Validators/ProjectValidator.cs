using FluentValidation;
using PageConstructor.Application.Projects.Models;

namespace PageConstructor.Infrastructure.Projects.Validators;

public class ProjectValidator : AbstractValidator<ProjectDto>
{
    public ProjectValidator()
    {
        RuleFor(project => project.Name)
            .NotNull().WithMessage("Name can't be null.")
            .NotEmpty().WithMessage("Name can't be empty.")
            .MinimumLength(1).WithMessage("Name is required.");
    }
}
