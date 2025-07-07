using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Components.Models;

public class ComponentFilter : FilterPagination
{
    /// <summary>
    /// Overrides base GetHashCode method
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageToken);
        hashCode.Add(PageSize);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Overrides base Equels method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) =>
        obj is ComponentFilter filter
        && filter.GetHashCode() == GetHashCode();
}
