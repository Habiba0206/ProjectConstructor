using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontDeleteByIdCommandHandler(
    IFontService fontService)
    : ICommandHandler<FontDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(FontDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await fontService.DeleteByIdAsync(request.FontId, cancellationToken: cancellationToken);

        return result is not null;
    }
}