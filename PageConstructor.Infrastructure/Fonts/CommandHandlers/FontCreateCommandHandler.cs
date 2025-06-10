using AutoMapper;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontCreateCommandHandler(
    IMapper mapper,
    IFontService fontService) : ICommandHandler<FontCreateCommand, FontDto>
{
    public async Task<FontDto> Handle(FontCreateCommand request, CancellationToken cancellationToken)
    {
        var font = mapper.Map<Font>(request.FontDto);

        var createdFont = await fontService.CreateAsync(font, cancellationToken: cancellationToken);

        return mapper.Map<FontDto>(createdFont);
    }
}
