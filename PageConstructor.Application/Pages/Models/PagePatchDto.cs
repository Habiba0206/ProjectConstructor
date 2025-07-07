namespace PageConstructor.Application.Pages.Models;

public class PagePatchDto
{
    public Guid Id { get; set; }

    public Guid? ProjectId { get; set; }
    public string? Title { get; set; }
    public string? UrlPath { get; set; }
    public string? Html { get; set; }
    public string? Css { get; set; }
    public bool? IsPublished { get; set; }
    public DateTimeOffset? LastSaved { get; set; }
}
