using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Metas.Commands;

public record MetaCreateCommand : ICommand<MetaDto>
{
    public MetaDto MetaDto { get; set; }
}
