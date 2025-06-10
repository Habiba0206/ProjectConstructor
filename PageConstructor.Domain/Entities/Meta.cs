using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Meta : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public IList<string> Keywords { get; set; }

    public Guid PageId { get; set; }
    public Page Page { get; set; }
}
