using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FontsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FontGetQuery fontGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(fontGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{fontId:guid}")]
    public async ValueTask<IActionResult> GetFontById([FromRoute] Guid fontId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontGetByIdQuery { FontId = fontId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateFont([FromBody] FontCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateFont([FromBody] FontUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{fontId:guid}")]
    public async ValueTask<IActionResult> DeleteFontById([FromRoute] Guid fontId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontDeleteByIdCommand { FontId = fontId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
