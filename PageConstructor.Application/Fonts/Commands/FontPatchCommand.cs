using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Common.Commands;

namespace PageConstructor.Application.Fonts.Commands;

public class FontPatchCommand : ICommand<FontPatchDto>
{
    public FontPatchDto FontPatchDto { get; set; }
}
