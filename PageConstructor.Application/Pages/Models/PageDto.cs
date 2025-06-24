using PageConstructor.Application.Fonts.Models;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Scripts.Models;

namespace PageConstructor.Application.Pages.Models;

public class PageDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string UrlPath { get; set; }
    public string? Css { get; set; }
    public bool IsPublished { get; set; }
    public DateTimeOffset LastSaved { get; set; }
    public Guid ProjectId { get; set; }
    public string? SectionsJson { get; set; }
    public IList<MetaDto> Metas { get; set; }
    public IList<ScriptDto> Scripts { get; set; }
    //public List<Style> Styles { get; set; }
    public IList<FontDto> Fonts { get; set; }
}
