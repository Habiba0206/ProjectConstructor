using FluentValidation;
using PageConstructor.Application.Scripts.Commands;

namespace PageConstructor.Infrastructure.Scripts.Validators;

public class ScriptCreateCommandValidator : AbstractValidator<ScriptCreateCommand>
{
    public ScriptCreateCommandValidator()
    {
        RuleFor(x => x.ScriptDto)
            .NotNull().WithMessage("ScriptDto cannot be null.")
            .SetValidator(new ScriptValidator());
    }
}
