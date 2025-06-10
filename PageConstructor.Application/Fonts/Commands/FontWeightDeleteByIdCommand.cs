using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public class FontWeightDeleteByIdCommand : ICommand<bool>
{
    public Guid FontWeightId { get; set; }
}
