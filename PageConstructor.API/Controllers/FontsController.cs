﻿using PageConstructor.Application.Fonts.Commands;
using PageConstructor.Application.Fonts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Pages.Models;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
}
