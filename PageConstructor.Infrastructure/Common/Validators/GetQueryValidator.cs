﻿using FluentValidation;
using Newtonsoft.Json.Linq;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Common.Validators;

public class GetQueryValidator: AbstractValidator<FilterPagination>
{
    public GetQueryValidator()
    {
        RuleFor(x => x.PageToken)
            .GreaterThan(0).WithMessage("Token must be greater than 0.")
            .When(x => x.PageToken.HasValue);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.")
            .When(x => x.PageSize.HasValue);
    }
}
