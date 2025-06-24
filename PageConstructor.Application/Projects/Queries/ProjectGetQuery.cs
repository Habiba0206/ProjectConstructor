using PageConstructor.Application.Projects.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Projects.Queries;

public class ProjectGetQuery : IQuery<ICollection<ProjectDto>>
{
    public ProjectFilter ProjectFilter { get; set; }
}
