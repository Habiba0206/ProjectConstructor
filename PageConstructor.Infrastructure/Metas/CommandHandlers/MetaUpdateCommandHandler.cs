using AutoMapper;
using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Metas.CommandHandlers;

public class MetaUpdateCommandHandler(
    IMapper mapper,
    IMetaService bookService) : ICommandHandler<MetaUpdateCommand, MetaDto>
{
    public async Task<MetaDto> Handle(MetaUpdateCommand request, CancellationToken cancellationToken)
    {
        var meta = mapper.Map<Meta>(request.MetaDto);

        var updatedMeta = await bookService.UpdateAsync(meta, cancellationToken: cancellationToken);

        return mapper.Map<MetaDto>(updatedMeta);
    }
}
