using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Metas.CommandHandlers;

public class MetaDeleteByIdCommandHandler(
    IMetaService bookService)
    : ICommandHandler<MetaDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(MetaDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await bookService.DeleteByIdAsync(request.MetaId, cancellationToken: cancellationToken);

        return result is not null;
    }
}