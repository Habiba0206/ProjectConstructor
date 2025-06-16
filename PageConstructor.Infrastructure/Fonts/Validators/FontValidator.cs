using FluentValidation;
using PageConstructor.Application.Fonts.Models;

namespace PageConstructor.Infrastructure.Fonts.Validators;
public class FontValidator : AbstractValidator<FontDto>
{
    public FontValidator()
    {
        RuleFor(font => font.Name).NotEmpty().WithMessage("Name can't be empty.");
        RuleFor(font => font.Src).NotEmpty().WithMessage("Src can't be empty.");
        RuleFor(font => font.Display).NotEmpty().WithMessage("Display can't be empty.");
    }
}
