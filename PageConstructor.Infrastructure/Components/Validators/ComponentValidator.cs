using FluentValidation;
using PageConstructor.Application.Components.Models;

namespace PageConstructor.Infrastructure.Components.Validators;

public class ComponentValidator : AbstractValidator<ComponentDto>
{
    public ComponentValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(c => c.HtmlContent)
            .NotEmpty().WithMessage("HTML content is required.");

        RuleFor(c => c.PreviewImageUrl)
            .NotEmpty().WithMessage("Preview image URL is required.");

        RuleFor(c => c.BlockId)
            .NotEmpty().WithMessage("Block ID must be specified.");
    }
}