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

public class FontWeightUpdateCommandHandler(
    IMapper mapper,
    IFontWeightService bookService,
    FontWeightValidator validator) : ICommandHandler<FontWeightUpdateCommand, FontWeightDto>
{
    public async Task<FontWeightDto> Handle(FontWeightUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.FontWeightDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var fontWeight = mapper.Map<FontWeight>(request.FontWeightDto);

        var updatedFontWeight = await bookService.UpdateAsync(fontWeight, cancellationToken: cancellationToken);

        return mapper.Map<FontWeightDto>(updatedFontWeight);
    }
}
