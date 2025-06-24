using FluentValidation;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Projects.Commands;

namespace PageConstructor.Infrastructure.Projects.Validators;

public class ProjectPatchCommandValidator : AbstractValidator<ProjectPatchCommand>
{
    public ProjectPatchCommandValidator()
    {
        RuleFor(x => x.ProjectPatchDto)
            .NotNull().WithMessage("Patch DTO must not be null.");

        RuleFor(x => x.ProjectPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}
