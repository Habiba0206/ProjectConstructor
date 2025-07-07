using PageConstructor.Domain.Common.Entities;

namespace PageConstructor.Domain.Entities;

public class Page : AuditableEntity
{
    public string Title { get; set; }
    public string UrlPath { get; set; }
    public string? Css { get; set; }
    public string Html { get; set; }
    public bool IsPublished { get; set; }
    public DateTimeOffset LastSaved { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public string? SectionsJson { get; set; }
    public IList<Meta> Metas { get; set; }
    public IList<Script> Scripts { get; set; }
    //public List<Style> Styles { get; set; }
    public IList<Font> Fonts { get; set; }
}
