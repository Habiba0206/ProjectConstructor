using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Metas.Validators;

namespace PageConstructor.Infrastructure.Metas.CommandHandlers;

public class MetaUpdateCommandHandler(
    IMapper mapper,
    IMetaService bookService,
    MetaValidator validator) : ICommandHandler<MetaUpdateCommand, MetaDto>
{
    public async Task<MetaDto> Handle(MetaUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.MetaDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var meta = mapper.Map<Meta>(request.MetaDto);

        var updatedMeta = await bookService.UpdateAsync(meta, cancellationToken: cancellationToken);

        return mapper.Map<MetaDto>(updatedMeta);
    }
}
