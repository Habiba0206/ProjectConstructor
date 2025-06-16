using FluentValidation;
using PageConstructor.Application.Metas.Commands;

namespace PageConstructor.Infrastructure.Metas.Validators;

public class MetaUpdateCommandValidator : AbstractValidator<MetaUpdateCommand>
{
    public MetaUpdateCommandValidator()
    {
        RuleFor(x => x.MetaDto)
            .NotNull().WithMessage("MetaDto cannot be null.")
            .SetValidator(new MetaValidator());
    }
}
