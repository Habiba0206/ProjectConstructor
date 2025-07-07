using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Common.Services;
using PageConstructor.Application.Components.Commands;
using PageConstructor.Application.Components.Models;
using PageConstructor.Application.Components.Queries;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ComponentsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all components with optional filtering and pagination.
    /// </summary>
    /// <param name="query">The query parameters for filtering components.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>A list of components or 204 No Content if none found.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ComponentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] ComponentGetQuery query, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a specific component by its unique identifier.
    /// </summary>
    /// <param name="componentId">The unique identifier of the component to retrieve.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>The component if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{componentId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ComponentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid componentId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ComponentGetByIdQuery { ComponentId = componentId }, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new component.
    /// </summary>
    /// <param name="command">The component creation command containing the necessary data.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>The created component details, or 400 Bad Request if creation failed.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ComponentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] ComponentCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates an existing component.
    /// </summary>
    /// <param name="command">The update command containing modified component data.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>The updated component details, or 400 Bad Request if update failed.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ComponentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Update([FromBody] ComponentUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Partially updates a component with specified fields.
    /// </summary>
    /// <param name="command">The patch command containing partial updates.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>The updated component details or 400 Bad Request if invalid.</returns>
    [HttpPatch]
    [ProducesResponseType(typeof(ComponentPatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Patch([FromBody] ComponentPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Deletes a component by its unique identifier.
    /// </summary>
    /// <param name="componentId">The ID of the component to delete.</param>
    /// <param name="cancellationToken">Token for cancelling the request.</param>
    /// <returns>200 OK if deleted; otherwise 400 Bad Request.</returns>
    [HttpDelete("{componentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid componentId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ComponentDeleteByIdCommand { ComponentId = componentId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Uploads a preview image for a component and returns the URL.
    /// </summary>
    /// <param name="uploadService">The file upload service used to store the image.</param>
    /// <param name="dto">The DTO that contains the uploaded file.</param>
    /// <returns>The uploaded image URL or an appropriate error code.</returns>
    [HttpPost("upload-preview")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> UploadPreview(
        [FromServices] IFileUploadService uploadService,
        [FromForm] ComponentImageUploadDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("No file uploaded.");

        var url = await uploadService.UploadComponentPreviewAsync(dto.File);
        return Ok(new { url });
    }
}
