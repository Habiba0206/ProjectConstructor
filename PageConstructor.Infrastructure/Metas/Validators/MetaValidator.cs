using FluentValidation;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Metas.Validators;
public class MetaValidator : AbstractValidator<MetaDto>
{
    public MetaValidator()
    {
        RuleFor(meta => meta.Title).NotEmpty().WithMessage("Title can't be empty.");
    }
}
