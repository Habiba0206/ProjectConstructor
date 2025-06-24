using AutoMapper;
using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Metas.CommandHandlers;

public class MetaPatchCommandHandler(
    IMetaService metaService,
    IMapper mapper)
    : ICommandHandler<MetaPatchCommand, MetaPatchDto>
{
    public async Task<MetaPatchDto> Handle(MetaPatchCommand request, CancellationToken cancellationToken)
    {
        var meta = await metaService.PatchAsync(request.MetaPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<MetaPatchDto>(meta);
    }
}
