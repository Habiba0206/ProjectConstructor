using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using FluentValidation;

namespace PageConstructor.Infrastructure.Fonts.Validators;
public class FontValidator : AbstractValidator<Font>
{
    public FontValidator()
    {
        RuleFor(font => font.Name).NotEmpty();
        RuleFor(font => font.Src).NotEmpty();
        RuleFor(font => font.Display).NotEmpty();
    }
}
