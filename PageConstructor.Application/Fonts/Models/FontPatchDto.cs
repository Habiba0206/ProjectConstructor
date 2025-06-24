namespace PageConstructor.Application.Fonts.Models;

public class FontPatchDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Src { get; set; }
    public string? Display { get; set; }
    public Guid? PageId { get; set; }
}
