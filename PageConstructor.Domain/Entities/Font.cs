using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Font : AuditableEntity
{
    public string Name { get; set; }
    public string Src { get; set; }
    public string Display { get; set; }

    public IList<FontWeight> Weights { get; set; }

    public Guid PageId { get; set; }
    public Page Page { get; set; }
}
