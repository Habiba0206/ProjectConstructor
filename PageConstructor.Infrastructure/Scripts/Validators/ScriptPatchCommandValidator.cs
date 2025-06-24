using FluentValidation;
using PageConstructor.Application.Scripts.Commands;

namespace PageConstructor.Infrastructure.Scripts.Validators;

public class ScriptPatchCommandValidator : AbstractValidator<ScriptPatchCommand>
{
    public ScriptPatchCommandValidator()
    {
        RuleFor(x => x.ScriptPatchDto)
            .NotNull().WithMessage("Script DTO must not be null.");

        RuleFor(x => x.ScriptPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}