using FluentValidation;
using PageConstructor.Application.Components.Commands;
using PageConstructor.Infrastructure.Components.Validators;

namespace PageConstructor.Infrastructure.Components.Validators;

public class ComponentUpdateCommandValidator : AbstractValidator<ComponentUpdateCommand>
{
    public ComponentUpdateCommandValidator()
    {
        RuleFor(x => x.ComponentDto)
            .NotNull().WithMessage("ComponentDto cannot be null.")
            .SetValidator(new ComponentValidator());
    }
}