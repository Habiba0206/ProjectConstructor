using FluentValidation;
using PageConstructor.Application.Fonts.Commands;

namespace PageConstructor.Infrastructure.Fonts.Validators;

public class FontUpdateCommantValidator : AbstractValidator<FontUpdateCommand>
{
    public FontUpdateCommantValidator()
    {
        RuleFor(x => x.FontDto)
            .NotNull().WithMessage("FontDto cannot be null.")
            .SetValidator(new FontValidator());
    }
}
