namespace PageConstructor.Application.Blocks.Models;

public class BlockDto
{
    public Guid? Id { get; set; }

    /// <summary>
    /// Display name for the block (e.g. "Hero Section")
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Category of the block (e.g. "Bootstrap", "Custom", "Layout")
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Icon or label to show in GrapesJS (can be an emoji or a class name)
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Raw HTML content for the block — used to insert into GrapesJS editor
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Optional CSS styles that should be applied when the block is added
    /// </summary>
    public string? Css { get; set; }

    /// <summary>
    /// Optional JS that runs when the block is rendered (advanced)
    /// </summary>
    public string? Script { get; set; }

    /// <summary>
    /// Whether this block should be visible to the user
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Optional preview image URL for visual block representation
    /// </summary>
    public string? PreviewImageUrl { get; set; }
}
