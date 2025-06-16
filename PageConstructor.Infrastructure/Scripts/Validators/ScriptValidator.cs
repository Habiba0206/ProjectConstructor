using FluentValidation;
using PageConstructor.Application.Scripts.Models;

namespace PageConstructor.Infrastructure.Scripts.Validators;
public class ScriptValidator : AbstractValidator<ScriptDto>
{
    public ScriptValidator()
    {
        RuleFor(script => script.Type).NotEmpty().WithMessage("Type can't be empty.");
        RuleFor(script => script.Src).NotEmpty().WithMessage("Src can't be empty.");
        RuleFor(script => script.Lang).NotEmpty().WithMessage("Language can't be empty.");
    }
}
