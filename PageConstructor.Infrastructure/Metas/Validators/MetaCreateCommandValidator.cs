using FluentValidation;
using PageConstructor.Application.Metas.Commands;

namespace PageConstructor.Infrastructure.Metas.Validators;

public class MetaCreateCommandValidator : AbstractValidator<MetaCreateCommand>
{
    public MetaCreateCommandValidator()
    {
        RuleFor(x => x.MetaDto)
            .NotNull().WithMessage("MetaDto cannot be null.")
            .SetValidator(new MetaValidator());
    }
}
