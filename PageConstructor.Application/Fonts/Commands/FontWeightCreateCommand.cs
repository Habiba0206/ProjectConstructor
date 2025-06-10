using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public record FontWeightCreateCommand : ICommand<FontWeightDto>
{
    public FontWeightDto FontWeightDto { get; set; }
}
