using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Script : AuditableEntity
{
    public string Type { get; set; }
    public string Src { get; set; }
    public string Lang { get; set; } = "css";
    public bool Modules { get; set; }
    public bool Async { get; set; }

    public Guid PageId { get; set; }
    public Page Page { get; set; }
}
