using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Fonts.Validators;

namespace PageConstructor.Infrastructure.Fonts.CommandHandlers;

public class FontUpdateCommandHandler(
    IMapper mapper,
    IFontService fontService,
    FontValidator validator) : ICommandHandler<FontUpdateCommand, FontDto>
{
    public async Task<FontDto> Handle(FontUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.FontDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var font = mapper.Map<Font>(request.FontDto);

        var updatedFont = await fontService.UpdateAsync(font, cancellationToken: cancellationToken);

        return mapper.Map<FontDto>(updatedFont);
    }
}
