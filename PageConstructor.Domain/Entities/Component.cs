using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Component : AuditableEntity
{
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public string Css { get; set; }
    public string PreviewImageUrl { get; set; }

    public Guid BlockId { get; set; }
    public Block Block { get; set; }
}
