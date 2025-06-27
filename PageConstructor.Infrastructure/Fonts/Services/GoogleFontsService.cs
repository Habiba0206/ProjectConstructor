using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Persistence.Extensions;

namespace PageConstructor.Infrastructure.Fonts.Services;

public class GoogleFontsService : IGoogleFontsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    private static readonly string[] AllowedSorts = { "alpha", "date", "popularity", "style", "trending" };

    public GoogleFontsService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["GoogleFonts:ApiKey"];
    }

    public async ValueTask<string> GetWebFontsJsonAsync(
        string? family = null,
        string? search = null,
        string? subset = null,
        string? sort = null,
        string? category = null,
        string? capability = null,
        FilterPagination? pagination = null,
        CancellationToken cancellationToken = default)
    {
        // Build base URL with supported params (key, family, sort)
        var url = $"https://www.googleapis.com/webfonts/v1/webfonts?key={_apiKey}";
        if (!string.IsNullOrWhiteSpace(sort))
        {
            if (!AllowedSorts.Contains(sort, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException($"Invalid sort '{sort}'. Allowed: {string.Join(", ", AllowedSorts)}");
            url += $"&sort={sort}";
        }
        if (!string.IsNullOrWhiteSpace(family))
            url += $"&family={Uri.EscapeDataString(family)}";

        // Call API
        var response = await _httpClient.GetStringAsync(url, cancellationToken);

        // Parse JSON
        using var doc = JsonDocument.Parse(response);
        var root = doc.RootElement;
        var items = root.GetProperty("items").EnumerateArray();

        //Filter
        var allFilteredItems = items
        .Where(item => string.IsNullOrWhiteSpace(search) ||
            item.GetProperty("family").GetString()!
                .Contains(search, StringComparison.OrdinalIgnoreCase))
        .Where(item => subset == null ||
            item.GetProperty("subsets").EnumerateArray()
                .Any(s => s.GetString()!.Equals(subset, StringComparison.OrdinalIgnoreCase)))
        .Where(item => category == null ||
            item.GetProperty("category").GetString()!
                .Equals(category, StringComparison.OrdinalIgnoreCase))
        .Where(item => capability == null ||
            item.GetProperty("files").EnumerateObject()
                .Any(f => capability.Equals("woff2")
                    ? f.Value.GetString()!.EndsWith(".woff2")
                    : f.Name.Equals("variable", StringComparison.OrdinalIgnoreCase)))
        .ToList();

        var totalCount = allFilteredItems.Count;

        //Pagination:
        var pagedItems = allFilteredItems
            .ApplyPagination(pagination)
            .ToList();

        // Construct new JSON with filtered items
        using var stream = new System.IO.MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        writer.WriteStartObject();
        writer.WriteString("kind", root.GetProperty("kind").GetString());
        writer.WriteNumber("totalCount", totalCount);
        writer.WriteNumber("pageSize", pagination?.PageSize ?? 20);
        writer.WriteNumber("pageToken", pagination?.PageToken ?? 1);
        writer.WritePropertyName("items");
        JsonSerializer.Serialize(writer, pagedItems);
        writer.WriteEndObject();
        writer.Flush();

        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
    }

    public async ValueTask<List<int>> GetFontWeightsAsync(
        string family, 
        CancellationToken cancellationToken = default)
    {
        var url = $"https://www.googleapis.com/webfonts/v1/webfonts?key={_apiKey}";

        var response = await _httpClient.GetStringAsync(url, cancellationToken);
        using var doc = JsonDocument.Parse(response);
        var root = doc.RootElement;
        var items = root.GetProperty("items").EnumerateArray();

        var font = items.FirstOrDefault(f =>
            f.GetProperty("family").GetString().Equals(family, StringComparison.OrdinalIgnoreCase));

        if (font.ValueKind == JsonValueKind.Undefined)
            return new List<int>(); // not found

        var variants = font.GetProperty("variants").EnumerateArray().Select(v => v.GetString()).ToList();

        // Map to numeric font weights
        var weights = new List<int>();
        foreach (var variant in variants)
        {
            if (variant == "regular" || variant == "italic")
                weights.Add(400);
            else if (int.TryParse(variant, out var value))
                weights.Add(value);
        }

        return weights.Distinct().OrderBy(w => w).ToList();
    }
}