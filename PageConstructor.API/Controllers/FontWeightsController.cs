using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FontWeightsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FontWeightGetQuery fontWeightGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(fontWeightGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{fontWeightId:guid}")]
    public async ValueTask<IActionResult> GetBookById([FromRoute] Guid fontWeightId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontWeightGetByIdQuery { FontWeightId = fontWeightId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] FontWeightCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] FontWeightUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{fontWeightId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid fontWeightId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontWeightDeleteByIdCommand { FontWeightId = fontWeightId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
