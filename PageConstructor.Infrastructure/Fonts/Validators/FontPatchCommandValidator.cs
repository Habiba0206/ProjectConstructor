using FluentValidation;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Pages.Commands;

namespace PageConstructor.Infrastructure.Fonts.Validators;

public class FontPatchCommandValidator : AbstractValidator<FontPatchCommand>
{
    public FontPatchCommandValidator()
    {
        RuleFor(x => x.FontPatchDto)
            .NotNull().WithMessage("Font DTO must not be null.");

        RuleFor(x => x.FontPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}
