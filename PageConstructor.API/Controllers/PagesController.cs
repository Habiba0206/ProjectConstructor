using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PageConstructor.Application.Pages.Models;
using PageConstructor.API.Common;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PagesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all pages.
    /// </summary>
    /// <param name="pageGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of pages or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<PageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] PageGetQuery pageGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(pageGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a page by its unique identifier.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the page details if found,  
    /// or <see cref="NotFoundResult"/> if the page does not exist.
    /// </returns>
    [HttpGet("by-id/{pageId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid pageId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new PageGetByIdQuery { PageId = pageId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Retrieves a page by its project id.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>List of pages or no 204 No Content.</returns>
    [HttpGet("by-project/{projectId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<List<PageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetByProjectId([FromRoute] Guid projectId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new PageGetByProjectIdQuery { ProjectId = projectId }, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Creates a new page.
    /// </summary>
    /// <param name="command">The page creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created page details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">Page created successfully.</response>
    /// <response code="400">Invalid input or page creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] PageCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing page.
    /// </summary>
    /// <param name="command">The page updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated page details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Page updated successfully.</response>
    /// <response code="400">Invalid input or page updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] PageUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing page.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated page data.</returns>
    /// <response code="200">Page patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(PagePatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] PagePatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a page by its unique identifier.
    /// </summary>
    /// <param name="pageId">The ID of the page to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{pageId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid pageId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new PageDeleteByIdCommand { PageId = pageId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
