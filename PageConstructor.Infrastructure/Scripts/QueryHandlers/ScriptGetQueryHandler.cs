using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Queries;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Scripts.QueryHandlers;

public class ScriptGetQueryHandler(
    IMapper mapper,
    IScriptService scriptService)
    : IQueryHandler<ScriptGetQuery, ICollection<ScriptDto>>
{
    public async Task<ICollection<ScriptDto>> Handle(ScriptGetQuery request, CancellationToken cancellationToken)
    {
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
