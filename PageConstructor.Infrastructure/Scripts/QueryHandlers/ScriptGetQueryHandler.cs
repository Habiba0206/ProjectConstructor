using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Queries;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Scripts.QueryHandlers;

public class ScriptGetQueryHandler(
    IMapper mapper,
    IScriptService scriptService,
    GetQueryValidator validationRules)
    : IQueryHandler<ScriptGetQuery, ICollection<ScriptDto>>
{
    public async Task<ICollection<ScriptDto>> Handle(ScriptGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.ScriptFilter ?? new ScriptFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await scriptService.Get(
            request.ScriptFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<ScriptDto>>(result);
    }
}
