using FluentValidation;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Metas.Validators;
public class MetaValidator : AbstractValidator<Meta>
{
    public MetaValidator()
    {
        RuleFor(meta => meta.Title).NotEmpty();
    }
}
