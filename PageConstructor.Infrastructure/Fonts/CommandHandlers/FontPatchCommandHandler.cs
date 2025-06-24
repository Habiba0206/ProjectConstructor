using AutoMapper;
using MassTransit.Serialization;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontPatchCommandHandler(
    IFontService fontService,
    IMapper mapper) 
    : ICommandHandler<FontPatchCommand, FontPatchDto>
{
    public async Task<FontPatchDto> Handle(FontPatchCommand request, CancellationToken cancellationToken)
    {
        var font = await fontService.PatchAsync(request.FontPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<FontPatchDto>(font);
    }
}
