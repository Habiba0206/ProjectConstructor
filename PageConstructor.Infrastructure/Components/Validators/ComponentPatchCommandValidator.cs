using FluentValidation;
using PageConstructor.Application.Components.Commands;

namespace PageConstructor.Infrastructure.Components.Validators;

public class ComponentPatchCommandValidator : AbstractValidator<ComponentPatchCommand>
{
    public ComponentPatchCommandValidator()
    {
        RuleFor(x => x.ComponentPatchDto)
            .NotNull().WithMessage("Patch DTO must not be null.");

        RuleFor(x => x.ComponentPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}