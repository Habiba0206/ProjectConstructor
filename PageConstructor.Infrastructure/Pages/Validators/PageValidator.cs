using FluentValidation;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Pages.Validators;
public class PageValidator : AbstractValidator<Page>
{
    public PageValidator()
    {
        RuleFor(page => page.Title).NotEmpty();
        RuleFor(page => page.ProjectId).NotEqual(Guid.Empty);
    }
}
