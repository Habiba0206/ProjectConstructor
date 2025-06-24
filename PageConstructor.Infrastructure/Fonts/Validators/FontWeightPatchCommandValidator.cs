using FluentValidation;
using PageConstructor.Application.Fonts.Commands;

namespace PageConstructor.Infrastructure.Fonts.Validators;

public class FontWeightPatchCommandValidator : AbstractValidator<FontWeightPatchCommand>
{
    public FontWeightPatchCommandValidator()
    {
        RuleFor(x => x.FontWeightPatchDto)
            .NotNull().WithMessage("FontWeight DTO must not be null.");

        RuleFor(x => x.FontWeightPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}