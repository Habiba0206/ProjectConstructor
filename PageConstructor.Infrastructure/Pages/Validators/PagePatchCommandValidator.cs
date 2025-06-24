using FluentValidation;
using PageConstructor.Application.Pages.Commands;

namespace PageConstructor.Infrastructure.Pages.Validatorsl;

public class PagePatchCommandValidator : AbstractValidator<PagePatchCommand>
{
    public PagePatchCommandValidator()
    {
        RuleFor(x => x.PagePatchDto)
            .NotNull().WithMessage("Patch DTO must not be null.");

        RuleFor(x => x.PagePatchDto.Id)
            .NotNull().WithMessage("Id can not be null.")
            .NotEmpty().WithMessage("Id is required for patching.");
    }
}