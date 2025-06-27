using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Project : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string UrlPath { get; set; } = default!;
    public string? GlobalStyles { get; set; }
    public IList<Page> Pages { get; set; }
}
