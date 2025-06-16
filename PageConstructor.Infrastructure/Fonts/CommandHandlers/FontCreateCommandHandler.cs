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

public class FontCreateCommandHandler(
    IMapper mapper,
    IFontService fontService,
    FontValidator validator) : ICommandHandler<FontCreateCommand, FontDto>
{
    public async Task<FontDto> Handle(FontCreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.FontDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var font = mapper.Map<Font>(request.FontDto);

        var createdFont = await fontService.CreateAsync(font, cancellationToken: cancellationToken);

        return mapper.Map<FontDto>(createdFont);
    }
}
