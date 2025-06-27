using FluentValidation;
using PageConstructor.Application.Blocks.Commands;

namespace PageConstructor.Infrastructure.Blocks.Validators;

public class BlockCreateCommandValidator : AbstractValidator<BlockCreateCommand>
{
    public BlockCreateCommandValidator()
    {
        RuleFor(x => x.BlockDto)
            .NotNull().WithMessage("BlockDto cannot be null.")
            .SetValidator(new BlockValidator());
    }
}
