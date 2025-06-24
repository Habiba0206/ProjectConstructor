namespace PageConstructor.Application.Metas.Models;

public class MetaPatchDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public IList<string>? Keywords { get; set; }
    public Guid? PageId { get; set; }
}