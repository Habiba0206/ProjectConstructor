using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Fonts.Services;

public interface IGoogleFontsService
{
    ValueTask<string> GetWebFontsJsonAsync(
        string? family = null,
        string? search = null,
        string? subset = null,
        string? sort = null,
        string? category = null,
        string? capability = null,
        FilterPagination? pagination = null,
        CancellationToken cancellationToken = default);

    ValueTask<List<int>> GetFontWeightsAsync(
        string family,
        CancellationToken cancellationToken = default);
}
