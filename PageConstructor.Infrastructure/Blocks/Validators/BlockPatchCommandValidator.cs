using FluentValidation;
using PageConstructor.Application.Blocks.Commands;

namespace PageConstructor.Infrastructure.Blocks.Validators;

public class BlockPatchCommandValidator : AbstractValidator<BlockPatchCommand>
{
    public BlockPatchCommandValidator()
    {
        RuleFor(x => x.BlockPatchDto)
            .NotNull().WithMessage("Patch DTO must not be null.");

        RuleFor(x => x.BlockPatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}
