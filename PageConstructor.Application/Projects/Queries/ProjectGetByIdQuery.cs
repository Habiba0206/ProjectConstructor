using PageConstructor.Application.Projects.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Projects.Queries;

public class ProjectGetByIdQuery : IQuery<ProjectDto?>
{
    public Guid ProjectId { get; set; }
}