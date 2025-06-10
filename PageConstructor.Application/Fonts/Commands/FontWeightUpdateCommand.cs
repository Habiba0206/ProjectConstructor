using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public class FontWeightUpdateCommand : ICommand<FontWeightDto>
{
    public FontWeightDto FontWeightDto { get; set; }
}
