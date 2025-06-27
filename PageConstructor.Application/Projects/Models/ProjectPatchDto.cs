namespace PageConstructor.Application.Projects.Models;

public class ProjectPatchDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UrlPath { get; set; }
    public string? GlobalStyles { get; set; }
}
