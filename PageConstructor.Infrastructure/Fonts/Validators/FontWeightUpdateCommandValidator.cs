using FluentValidation;
using PageConstructor.Application.Fonts.Commands;

namespace PageConstructor.Infrastructure.Fonts.Validators;

public class FontWeightUpdateCommandValidator : AbstractValidator<FontWeightUpdateCommand>
{
    public FontWeightUpdateCommandValidator()
    {
        RuleFor(x => x.FontWeightDto)
            .NotNull().WithMessage("FontWeightDto cannot be null.")
            .SetValidator(new FontWeightValidator());
    }
}
