using FluentValidation;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.Validators;
public class FontWeightValidator : AbstractValidator<FontWeight>
{
    public FontWeightValidator()
    {
        RuleFor(fontWeight => fontWeight.FontId).NotEqual(Guid.Empty);
    }
}
