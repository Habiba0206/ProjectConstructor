using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Metas.Queries;

public class MetaGetQuery : IQuery<ICollection<MetaDto>>
{
    public MetaFilter MetaFilter { get; set; }
}
