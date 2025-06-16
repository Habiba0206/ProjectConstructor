using FluentValidation;
using PageConstructor.Application.Pages.Commands;

namespace PageConstructor.Infrastructure.Pages.Validators;

public class PageUpdateCommandValidator : AbstractValidator<PageUpdateCommand>
{
    public PageUpdateCommandValidator()
    {
        RuleFor(x => x.PageDto)
            .NotNull().WithMessage("PageDto cannot be null.")
            .SetValidator(new PageValidator());
    }
}
