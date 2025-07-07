using Microsoft.AspNetCore.Http;

namespace PageConstructor.Application.Blocks.Services;

public interface IFileUploadService
{
    ValueTask<string> UploadBlockPreviewAsync(IFormFile file);
}
