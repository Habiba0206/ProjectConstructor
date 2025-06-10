using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class FontWeight : AuditableEntity
{
    public int Count { get; set; }
    public Guid FontId { get; set; }
    public Font Font { get; set; }
}
