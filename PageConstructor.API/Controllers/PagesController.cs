using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] PageGetQuery pageGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(pageGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{pageId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid pageId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new PageGetByIdQuery { PageId = pageId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] PageCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] PageUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{pageId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid pageId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new PageDeleteByIdCommand { PageId = pageId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
