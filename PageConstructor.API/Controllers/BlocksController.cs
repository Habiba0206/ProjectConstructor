using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Common;
using PageConstructor.Application.Blocks.Commands;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Queries;
using PageConstructor.Application.Common.Services;

namespace PageConstructor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BlocksController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all blocks.
    /// </summary>
    /// <param name="blockGetQuery">Filtering and pagination.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>List of blocks or no 204 No Content.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<BlockDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> Get([FromQuery] BlockGetQuery blockGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(blockGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves a block by its unique identifier.
    /// </summary>
    /// <param name="blockId">The unique identifier of the block to retrieve.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with the block details if found,  
    /// or <see cref="NotFoundResult"/> if the block does not exist.
    /// </returns>
    [HttpGet("{blockId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<BlockDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid blockId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new BlockGetByIdQuery { BlockId = blockId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Creates a new block.
    /// </summary>
    /// <param name="command">The block creation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with created block details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the creation failed.
    /// </returns>
    /// <response code="200">Block created successfully.</response>
    /// <response code="400">Invalid input or block creation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(BlockDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Create([FromBody] BlockCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Updates the existing block.
    /// </summary>
    /// <param name="command">The block updation command containing title, description, and other properties.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> with updated block details if successful;  
    /// otherwise returns <see cref="BadRequestResult"/> if the updation failed.
    /// </returns>
    /// <response code="200">Block updated successfully.</response>
    /// <response code="400">Invalid input or block updation failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(BlockDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Update([FromBody] BlockUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Partially updates an existing block.
    /// </summary>
    /// <param name="command">The patch command containing updated fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated block data.</returns>
    /// <response code="200">Block patched successfully</response>
    /// <response code="400">Invalid data in patch request</response>
    [HttpPatch]
    [ProducesResponseType(typeof(BlockPatchDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Patch([FromBody] BlockPatchCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a block by its unique identifier.
    /// </summary>
    /// <param name="blockId">The ID of the block to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpDelete("{blockId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid blockId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new BlockDeleteByIdCommand { BlockId = blockId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Uploads a preview image for a block and returns the file URL.
    /// </summary>
    /// <param name="uploadService">The file upload service (injected).</param>
    /// <param name="dto">The dto model where there is file to upload.</param>
    /// <returns>A URL pointing to the uploaded image.</returns>
    /// <response code="200">File uploaded successfully</response>
    /// <response code="400">No file was uploaded or file is empty</response>
    /// <response code="500">An unexpected error occurred while uploading</response>
    [HttpPost("upload-preview")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> UploadPreview([FromServices]IFileUploadService uploadService, [FromForm] BlockImageUploadDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("No file uploaded.");

        var url = await uploadService.UploadBlockPreviewAsync(dto.File);
        return Ok(new { url });
    }
}