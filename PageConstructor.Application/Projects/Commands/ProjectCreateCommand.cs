using PageConstructor.Application.Projects.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Projects.Commands;

public record ProjectCreateCommand : ICommand<ProjectDto>
{
    public ProjectDto ProjectDto { get; set; }
}
