using FluentValidation;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.Validators;
public class FontWeightValidator : AbstractValidator<FontWeightDto>
{
    public FontWeightValidator()
    {
        RuleFor(fontWeight => fontWeight.FontId)
            .NotNull().WithMessage("Font Id can't be null.")
            .NotEqual(Guid.Empty).WithMessage("Font Id can't +be empty.");
    }
}
