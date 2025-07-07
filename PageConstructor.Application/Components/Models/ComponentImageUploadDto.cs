using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PageConstructor.Application.Components.Models;

public class ComponentImageUploadDto
{
    [Required]
    public IFormFile File { get; set; } = default!;
}
