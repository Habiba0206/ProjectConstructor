using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PageConstructor.Application.Blocks.Models;

public class BlockImageUploadDto
{
    [Required]
    public IFormFile File { get; set; } = default!;
}
