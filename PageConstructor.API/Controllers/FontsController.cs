using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Infrastructure.Fonts.Services;
using PageConstructor.Application.Fonts.Services;
using System.ComponentModel.DataAnnotations;
using PageConstructor.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FontsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all fonts
    /// </summary>
    /// <param name="fontGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of fonts or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<FontDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] FontGetQuery fontGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(fontGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a font by its unique identifier.
    /// </summary>
    /// <param name="fontId">The unique identifier of the font to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the font details if found,  
    /// or <see cref="NotFoundResult"/> if the font does not exist.
    /// </returns>
    [HttpGet("{fontId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetFontById([FromRoute] Guid fontId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontGetByIdQuery { FontId = fontId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new font.
    /// </summary>
    /// <param name="command">The font creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created font details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">Font created successfully.</response>
    /// <response code="400">Invalid input or page creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FontDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateFont([FromBody] FontCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing font.
    /// </summary>
    /// <param name="command">The font updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated font details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Font updated successfully.</response>
    /// <response code="400">Invalid input or page updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(FontDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> UpdateFont([FromBody] FontUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing font.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated Font data.</returns>
    /// <response code="200">Font patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(FontPatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] FontPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a font by its unique identifier.
    /// </summary>
    /// <param name="fontId">The ID of the font to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{fontId:guid}")]
    public async ValueTask<IActionResult> DeleteFontById([FromRoute] Guid fontId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FontDeleteByIdCommand { FontId = fontId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Retrieves Google Fonts with optional filters.
    /// Search.
    /// Family: exact font family (e.g., "Roboto").
    /// Subset: character subset ("latin", "cyrillic").
    /// Sort: alpha, date, popularity, style, trending.
    /// Category: serif, sans-serif, monospace, display, handwriting.
    /// Capability: woff2, vf.
    /// </summary>
    [HttpGet("google")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetGoogleFonts(
        [FromServices] IGoogleFontsService googleService,
        [FromQuery] string? search = null,
        [FromQuery] string? family = null,
        [FromQuery] string? subset = null,
        [FromQuery, RegularExpression("^(alpha|date|popularity|style|trending)$")] string? sort = null,
        [FromQuery, RegularExpression("^(serif|sans-serif|monospace|display|handwriting)$")] string? category = null,
        [FromQuery, RegularExpression("^(woff2|vf)$")] string? capability = null,
        [FromQuery] FilterPagination? pagination = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var json = await googleService.GetWebFontsJsonAsync(
                family, search, subset, sort, category, capability, pagination, cancellationToken);
            return Content(json, "application/json");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
