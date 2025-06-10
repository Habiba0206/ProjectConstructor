using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Metas.Queries;

public class MetaGetByIdQuery : IQuery<MetaDto?>
{
    public Guid MetaId { get; set; }
}
