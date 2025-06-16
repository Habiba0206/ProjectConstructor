using FluentValidation;
using PageConstructor.Application.Scripts.Commands;

namespace PageConstructor.Infrastructure.Scripts.Validators;

public class ScriptUpdateCommandValidator : AbstractValidator<ScriptUpdateCommand>
{
    public ScriptUpdateCommandValidator()
    {
        RuleFor(x => x.ScriptDto)
            .NotNull().WithMessage("ScriptDto cannot be null.")
            .SetValidator(new ScriptValidator());
    }
}

