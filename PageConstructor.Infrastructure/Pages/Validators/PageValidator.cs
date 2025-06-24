using FluentValidation;
using PageConstructor.Application.Pages.Models;

namespace PageConstructor.Infrastructure.Pages.Validators;
public class PageValidator : AbstractValidator<PageDto>
{
    public PageValidator()
    {
        RuleFor(page => page.Title)
            .NotNull().WithMessage("Title can't be null.")
            .NotEmpty().WithMessage("Title can't be empty.")
            .MinimumLength(1).WithMessage("Title is required.");

        RuleFor(page => page.UrlPath)
            .NotEmpty().WithMessage("Url Path can't be empty");

        RuleFor(page => page.ProjectId)
            .NotNull().WithMessage("Project ID must not be null.")
            .NotEqual(Guid.Empty).WithMessage("Project ID must not be empty.");
    }
}
