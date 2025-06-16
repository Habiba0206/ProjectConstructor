using PageConstructor.Application.Metas.Commands;
using PageConstructor.Application.Metas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Pages.Models;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetasController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get all metas.
    /// </summary>
    /// <param name="metaGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of metas or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<MetaDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] MetaGetQuery metaGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(metaGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a meta by its unique identifier.
    /// </summary>
    /// <param name="metaId">The unique identifier of the meta to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the meta details if found,  
    /// or <see cref="NotFoundResult"/> if the meta does not exist.
    /// </returns>
    [HttpGet("{metaId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid metaId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new MetaGetByIdQuery { MetaId = metaId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound("[]");
    }

    /// <summary>
    /// Creates a new meta.
    /// </summary>
    /// <param name="command">The meta creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created meta details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">Meta created successfully.</response>
    /// <response code="400">Invalid input or page creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(MetaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] MetaCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing meta.
    /// </summary>
    /// <param name="command">The meta updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated meta details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Meta updated successfully.</response>
    /// <response code="400">Invalid input or page updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(MetaDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] MetaUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a meta tag by its unique identifier.
    /// </summary>
    /// <param name="metaId">The ID of the meta tag to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{metaId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid metaId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new MetaDeleteByIdCommand { MetaId = metaId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
