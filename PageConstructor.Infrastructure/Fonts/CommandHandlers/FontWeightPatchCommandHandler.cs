using AutoMapper;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontWeightPatchCommandHandler(
    IFontWeightService fontWeightService,
    IMapper mapper)
    : ICommandHandler<FontWeightPatchCommand, FontWeightPatchDto>
{
    public async Task<FontWeightPatchDto> Handle(FontWeightPatchCommand request, CancellationToken cancellationToken)
    {
        var fontWeight = await fontWeightService.PatchAsync(request.FontWeightPatchDto, cancellationToken: cancellationToken);

        return mapper.Map<FontWeightPatchDto>(fontWeight);
    }
}
