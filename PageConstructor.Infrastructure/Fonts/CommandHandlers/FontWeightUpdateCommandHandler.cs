using AutoMapper;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontWeightUpdateCommandHandler(
IMapper mapper,
    IFontWeightService bookService) : ICommandHandler<FontWeightUpdateCommand, FontWeightDto>
{
    public async Task<FontWeightDto> Handle(FontWeightUpdateCommand request, CancellationToken cancellationToken)
    {
        var fontWeight = mapper.Map<FontWeight>(request.FontWeightDto);

        var updatedFontWeight = await bookService.UpdateAsync(fontWeight, cancellationToken: cancellationToken);

        return mapper.Map<FontWeightDto>(updatedFontWeight);
    }
}
