using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Fonts.Services;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FontWeightsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all font weights.
    /// </summary>
    /// <param name="fontWeightGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of font weights or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<FontWeightDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] FontWeightGetQuery fontWeightGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(fontWeightGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a fontWeight by its unique identifier.
    /// </summary>
    /// <param name="fontWeightId">The unique identifier of the fontWeight to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the fontWeight details if found,  
    /// or <see cref="NotFoundResult"/> if the fontWeight does not exist.
    /// </returns>
    [HttpGet("{fontWeightId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetBookById([FromRoute] Guid fontWeightId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontWeightGetByIdQuery { FontWeightId = fontWeightId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new fontWeight.
    /// </summary>
    /// <param name="command">The fontWeight creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created fontWeight details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">FontWeight created successfully.</response>
    /// <response code="400">Invalid input or page creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FontWeightDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] FontWeightCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing fontWeight.
    /// </summary>
    /// <param name="command">The fontWeight updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated fontWeight details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">FontWeight updated successfully.</response>
    /// <response code="400">Invalid input or page updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(FontWeightDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] FontWeightUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing fontWeight.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated FontWeight data.</returns>
    /// <response code="200">FontWeight patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(FontWeightPatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] FontWeightPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a font weight by its unique identifier.
    /// </summary>
    /// <param name="fontWeightId">The ID of the font weight to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{fontWeightId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid fontWeightId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontWeightDeleteByIdCommand { FontWeightId = fontWeightId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Retrieves available font weights for a specific Google Font family.
    /// Example: /api/fonts/google/weights?family=Roboto
    /// </summary>
    [HttpGet("google")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetFontWeights(
        [FromServices] IGoogleFontsService googleService,
        [FromQuery] string family,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(family))
            return BadRequest(new { Error = "Font family is required." });

        var weights = await googleService.GetFontWeightsAsync(family, cancellationToken);

        if (weights == null || weights.Count == 0)
            return NotFound(new { Error = $"Font '{family}' not found or has no weight variants." });

        return Ok(new
        {
            font = family,
            weights
        });
    }
}
