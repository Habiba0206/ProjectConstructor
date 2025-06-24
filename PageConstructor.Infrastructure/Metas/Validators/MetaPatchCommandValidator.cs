using FluentValidation;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Metas.Commands;

namespace PageConstructor.Infrastructure.Metas.Validators;

public class MetaPatchCommandValidator : AbstractValidator<MetaPatchCommand>
{
    public MetaPatchCommandValidator()
    {
        RuleFor(x => x.MetaPatchDto)
            .NotNull().WithMessage("Meta DTO must not be null.");

        RuleFor(x => x.MetaPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}