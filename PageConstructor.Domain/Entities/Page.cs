using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Page : AuditableEntity
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string? Css { get; set; }
    public bool IsPublished { get; set; }

    public IList<Meta> Metas { get; set; }
    public IList<Script> Scripts { get; set; }
    //public List<Style> Styles { get; set; }
    public IList<Font> Fonts { get; set; }
}
