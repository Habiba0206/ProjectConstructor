using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Pages.Commands;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Pages.Queries;
using PageConstructor.Application.Projects.Commands;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Queries;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <param name="projectGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of projects or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ProjectDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] ProjectGetQuery projectGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(projectGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a project by its unique identifier.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the project details if found,  
    /// or <see cref="NotFoundResult"/> if the project does not exist.
    /// </returns>
    [HttpGet("{projectId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProjectDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid projectId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ProjectGetByIdQuery { ProjectId = projectId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="command">The project creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created project details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">Project created successfully.</response>
    /// <response code="400">Invalid input or project creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] ProjectCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing project.
    /// </summary>
    /// <param name="command">The project updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated project details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Project updated successfully.</response>
    /// <response code="400">Invalid input or project updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] ProjectUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing project.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated project data.</returns>
    /// <response code="200">Project patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(ProjectPatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] ProjectPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a project by its unique identifier.
    /// </summary>
    /// <param name="projectId">The ID of the project to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{projectId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid projectId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ProjectDeleteByIdCommand { ProjectId = projectId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}

