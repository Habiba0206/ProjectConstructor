using AutoMapper;
using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Metas.CommandHandlers;

public class MetaCreateCommandHandler(
    IMapper mapper,
    IMetaService metaService) : ICommandHandler<MetaCreateCommand, MetaDto>
{
    public async Task<MetaDto> Handle(MetaCreateCommand request, CancellationToken cancellationToken)
    {
        var meta = mapper.Map<Meta>(request.MetaDto);

        var createdMeta = await metaService.CreateAsync(meta, cancellationToken: cancellationToken);

        return mapper.Map<MetaDto>(createdMeta);
    }
}
