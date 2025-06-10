using AutoMapper;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontUpdateCommandHandler(
    IMapper mapper,
    IFontService fontService) : ICommandHandler<FontUpdateCommand, FontDto>
{
    public async Task<FontDto> Handle(FontUpdateCommand request, CancellationToken cancellationToken)
    {
        var font = mapper.Map<Font>(request.FontDto);

        var updatedFont = await fontService.UpdateAsync(font, cancellationToken: cancellationToken);

        return mapper.Map<FontDto>(updatedFont);
    }
}
