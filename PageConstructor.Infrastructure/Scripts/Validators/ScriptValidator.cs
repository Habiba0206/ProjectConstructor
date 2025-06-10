using FluentValidation;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Scripts.Validators;
public class ScriptValidator : AbstractValidator<Script>
{
    public ScriptValidator()
    {
        RuleFor(script => script.Type).NotEmpty();
        RuleFor(script => script.Src).NotEmpty();
        RuleFor(script => script.Lang).NotEmpty();
    }
}
