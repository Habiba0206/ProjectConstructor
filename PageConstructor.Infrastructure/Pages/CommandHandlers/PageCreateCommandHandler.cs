using AutoMapper;
using FluentValidation;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Entities;
using PageConstructor.Domain.Enums;
using PageConstructor.Infrastructure.Pages.Validators;

namespace PageConstructor.Infrastructure.Pages.CommandHandlers;

public class PageCreateCommandHandler(
    IMapper mapper,
    IPageService pageService,
    PageValidator validator) : ICommandHandler<PageCreateCommand, PageDto>
{
    public async Task<PageDto> Handle(PageCreateCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(
            request.PageDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var page = mapper.Map<Page>(request.PageDto);

        var createdPage = await pageService.CreateAsync(page, cancellationToken: cancellationToken);

        return mapper.Map<PageDto>(createdPage);
    }
}
