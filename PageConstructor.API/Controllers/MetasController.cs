using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] MetaGetQuery metaGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(metaGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{metaId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid metaId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new MetaGetByIdQuery { MetaId = metaId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] MetaCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] MetaUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{metaId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid metaId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new MetaDeleteByIdCommand { MetaId = metaId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
