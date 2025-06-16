using FluentValidation;
using PageConstructor.Application.Pages.Commands;

namespace PageConstructor.Infrastructure.Pages.Validators;

public class PageCreateCommandValidator : AbstractValidator<PageCreateCommand>
{
    public PageCreateCommandValidator()
    {
        RuleFor(x => x.PageDto)
            .NotNull().WithMessage("PageDto cannot be null.")
            .SetValidator(new PageValidator());
    }
}
