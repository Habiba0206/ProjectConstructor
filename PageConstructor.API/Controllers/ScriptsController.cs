using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScriptsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] ScriptGetQuery scriptGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(scriptGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{scriptId:guid}")]
    public async ValueTask<IActionResult> GetBookById([FromRoute] Guid scriptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ScriptGetByIdQuery { ScriptId = scriptId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] ScriptCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ScriptUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{scriptId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid scriptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ScriptDeleteByIdCommand { ScriptId = scriptId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
