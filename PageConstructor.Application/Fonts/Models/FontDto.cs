using PageConstructor.Domain.Entities;

namespace PageConstructor.Application.Fonts.Models;

public class FontDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Src { get; set; }
    public string Display { get; set; }

    public IList<FontWeightDto> Weights { get; set; }

    public Guid PageId { get; set; }
}
