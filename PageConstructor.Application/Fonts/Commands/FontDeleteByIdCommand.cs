using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public class FontDeleteByIdCommand : ICommand<bool>
{
    public Guid FontId { get; set; }
}
