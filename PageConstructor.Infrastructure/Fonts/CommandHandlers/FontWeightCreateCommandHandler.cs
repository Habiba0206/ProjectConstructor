using AutoMapper;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontWeightCreateCommandHandler(
    IMapper mapper,
    IFontWeightService fontWeightService) : ICommandHandler<FontWeightCreateCommand, FontWeightDto>
{
    public async Task<FontWeightDto> Handle(FontWeightCreateCommand request, CancellationToken cancellationToken)
    {
        var fontWeight = mapper.Map<FontWeight>(request.FontWeightDto);

        var createdFontWeight = await fontWeightService.CreateAsync(fontWeight, cancellationToken: cancellationToken);

        return mapper.Map<FontWeightDto>(createdFontWeight);
    }
}
