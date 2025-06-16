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

public class FontWeightCreateCommandHandler(
    IMapper mapper,
    IFontWeightService fontWeightService,
    FontWeightValidator validator) : ICommandHandler<FontWeightCreateCommand, FontWeightDto>
{
    public async Task<FontWeightDto> Handle(FontWeightCreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.FontWeightDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var fontWeight = mapper.Map<FontWeight>(request.FontWeightDto);

        var createdFontWeight = await fontWeightService.CreateAsync(fontWeight, cancellationToken: cancellationToken);

        return mapper.Map<FontWeightDto>(createdFontWeight);
    }
}
