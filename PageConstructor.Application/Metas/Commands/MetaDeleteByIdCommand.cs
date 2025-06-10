using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Metas.Commands;

public class MetaDeleteByIdCommand : ICommand<bool>
{
    public Guid MetaId { get; set; }
}
