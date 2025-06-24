using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public class FontWeightPatchCommand : ICommand<FontWeightPatchDto>
{
    public FontWeightPatchDto FontWeightPatchDto { get; set; }
}
