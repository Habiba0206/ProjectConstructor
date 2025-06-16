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

public class PageUpdateCommandHandler(
    IMapper mapper,
    IPageService pageService,
    PageValidator validator) : ICommandHandler<PageUpdateCommand, PageDto>
{
    public async Task<PageDto> Handle(PageUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request.PageDto,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var book = mapper.Map<Page>(request.PageDto);

        var updatedPage = await pageService.UpdateAsync(book, cancellationToken: cancellationToken);

        return mapper.Map<PageDto>(updatedPage);
    }
}
