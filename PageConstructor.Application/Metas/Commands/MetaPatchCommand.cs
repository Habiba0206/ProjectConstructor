using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Metas.Commands;

public class MetaPatchCommand : ICommand<MetaPatchDto>
{
    public MetaPatchDto MetaPatchDto { get; set; }
}
