using AutoMapper;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Queries;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Fonts.QueryHandlers;

public class FontGetByIdQueryHandler(
    IMapper mapper,
    IFontService bookService)
    : IQueryHandler<FontGetByIdQuery, FontDto>
{
    public async Task<FontDto> Handle(FontGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await bookService.GetByIdAsync(request.FontId, cancellationToken: cancellationToken);

        return mapper.Map<FontDto>(result);
    }
}
