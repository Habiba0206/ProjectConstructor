using PageConstructor.Domain.Common.Commands;
using PageConstructor.Application.Fonts.Models;

namespace PageConstructor.Application.Fonts.Commands;

public class FontUpdateCommand : ICommand<FontDto>
{
    public FontDto FontDto { get; set; }
}
