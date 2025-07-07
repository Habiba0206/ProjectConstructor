using Microsoft.AspNetCore.Http;

namespace PageConstructor.Application.Common.Services;

public interface IFileUploadService
{
    ValueTask<string> UploadBlockPreviewAsync(IFormFile file);

    ValueTask<string> UploadComponentPreviewAsync(IFormFile file);
}
