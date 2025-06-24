using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Projects.Commands;

public class ProjectDeleteByIdCommand : ICommand<bool>
{
    public Guid ProjectId { get; set; }
}
