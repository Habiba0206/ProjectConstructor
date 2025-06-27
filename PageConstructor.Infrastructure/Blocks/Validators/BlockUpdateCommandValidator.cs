using FluentValidation;
using PageConstructor.Application.Blocks.Commands;

namespace PageConstructor.Infrastructure.Blocks.Validators;

public class BlockUpdateCommandValidator : AbstractValidator<BlockUpdateCommand>
{
    public BlockUpdateCommandValidator()
    {
        RuleFor(x => x.BlockDto)
            .NotNull().WithMessage("BlockDto cannot be null.")
            .SetValidator(new BlockValidator());
    }
}