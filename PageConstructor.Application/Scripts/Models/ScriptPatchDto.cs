namespace PageConstructor.Application.Scripts.Models;

public class ScriptPatchDto
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Src { get; set; }
    public string? Lang { get; set; }
    public bool? Modules { get; set; }
    public bool? Async { get; set; }
    public Guid? PageId { get; set; }
}