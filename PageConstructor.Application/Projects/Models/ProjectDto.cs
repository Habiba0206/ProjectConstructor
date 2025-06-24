using PageConstructor.Application.Pages.Models;

namespace PageConstructor.Application.Projects.Models;

public class ProjectDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public IList<PagePatchDto> Pages { get; set; }
}
