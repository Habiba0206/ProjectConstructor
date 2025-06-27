using FluentValidation;
using PageConstructor.Application.Blocks.Models;

namespace PageConstructor.Infrastructure.Blocks.Validators;

public class BlockValidator : AbstractValidator<BlockDto>
{
    public BlockValidator()
    {
        RuleFor(block => block.Name)
            .NotEmpty().WithMessage("Block name is required.")
            .MaximumLength(100).WithMessage("Block name must be 100 characters or fewer.");

        RuleFor(block => block.Content)
            .NotEmpty().WithMessage("Content (HTML) is required.");

        RuleFor(block => block.Category)
            .MaximumLength(50).WithMessage("Category must be 50 characters or fewer.")
            .When(block => !string.IsNullOrWhiteSpace(block.Category));

        RuleFor(block => block.Label)
            .MaximumLength(50).WithMessage("Label must be 50 characters or fewer.")
            .When(block => !string.IsNullOrWhiteSpace(block.Label));

        RuleFor(block => block.Css)
            .MaximumLength(10000).WithMessage("CSS is too long.")
            .When(block => !string.IsNullOrWhiteSpace(block.Css));

        RuleFor(block => block.Script)
            .MaximumLength(10000).WithMessage("Script is too long.")
            .When(block => !string.IsNullOrWhiteSpace(block.Script));
    }
}
