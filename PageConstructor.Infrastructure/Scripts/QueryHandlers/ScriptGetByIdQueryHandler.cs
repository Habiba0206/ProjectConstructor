using AutoMapper;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Scripts.Queries;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Scripts.QueryHandlers;

public class ScriptGetByIdQueryHandler(
    IMapper mapper,
    IScriptService scriptService)
    : IQueryHandler<ScriptGetByIdQuery, ScriptDto>
{
    public async Task<ScriptDto> Handle(ScriptGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await scriptService.GetByIdAsync(request.ScriptId, cancellationToken: cancellationToken);

        return mapper.Map<ScriptDto>(result);
    }
}
