using PageConstructor.Application.Projects.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Projects.Commands;

public class ProjectPatchCommand : ICommand<ProjectPatchDto>
{
    public ProjectPatchDto ProjectPatchDto { get; set; } = null!;
}
