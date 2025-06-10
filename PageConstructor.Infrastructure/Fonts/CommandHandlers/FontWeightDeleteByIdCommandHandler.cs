using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontWeightDeleteByIdCommandHandler(
    IFontWeightService fontWeightService)
    : ICommandHandler<FontWeightDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(FontWeightDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await fontWeightService.DeleteByIdAsync(request.FontWeightId, cancellationToken: cancellationToken);

        return result is not null;
    }
}