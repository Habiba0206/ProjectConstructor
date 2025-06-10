using AutoMapper;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontWeightGetByIdQueryHandler(
    IMapper mapper,
    IFontWeightService fontWeightService)
    : IQueryHandler<FontWeightGetByIdQuery, FontWeightDto>
{
    public async Task<FontWeightDto> Handle(FontWeightGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await fontWeightService.GetByIdAsync(request.FontWeightId, cancellationToken: cancellationToken);

        await fontWeightService.UpdateAsync(result);

        return mapper.Map<FontWeightDto>(result);
    }
}
