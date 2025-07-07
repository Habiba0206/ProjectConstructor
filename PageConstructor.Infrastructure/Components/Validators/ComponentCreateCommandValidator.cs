using FluentValidation;
using PageConstructor.Application.Components.Commands;
using PageConstructor.Infrastructure.Components.Validators;

namespace PageConstructor.Infrastructure.Components.Validators;

public class ComponentCreateCommandValidator : AbstractValidator<ComponentCreateCommand>
{
    public ComponentCreateCommandValidator()
    {
        RuleFor(x => x.ComponentDto)
            .NotNull().WithMessage("ComponentDto cannot be null.")
            .SetValidator(new ComponentValidator());
    }
}
