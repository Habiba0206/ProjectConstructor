using PageConstructor.Application.Scripts.Commands;
using PageConstructor.Application.Scripts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Models;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScriptsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get all scripts.
    /// </summary>
    /// <param name="scriptGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of scripts or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ScriptDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] ScriptGetQuery scriptGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(scriptGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a script by its unique identifier.
    /// </summary>
    /// <param name="scriptId">The unique identifier of the script to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the script details if found,  
    /// or <see cref="NotFoundResult"/> if the script does not exist.
    /// </returns>
    [HttpGet("{scriptId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetBookById([FromRoute] Guid scriptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ScriptGetByIdQuery { ScriptId = scriptId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new script.
    /// </summary>
    /// <param name="command">The script creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created fontWeight details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the script failed.
    /// </returns>
    /// <response code="200">Script created successfully.</response>
    /// <response code="400">Invalid input or page creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] ScriptCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing script.
    /// </summary>
    /// <param name="command">The script updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated script details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Script updated successfully.</response>
    /// <response code="400">Invalid input or page updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] ScriptUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing script.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated Script data.</returns>
    /// <response code="200">Script patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(ScriptPatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] ScriptPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a script by its unique identifier.
    /// </summary>
    /// <param name="scriptId">The ID of the script to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{scriptId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid scriptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ScriptDeleteByIdCommand { ScriptId = scriptId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
