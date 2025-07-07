namespace PageConstructor.Application.Components.Models;

public class ComponentDto
{
    public Guid? Id { get; set; }
    public Guid BlockId { get; set; }
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public string Css { get; set; }
    public string PreviewImageUrl { get; set; }
}
