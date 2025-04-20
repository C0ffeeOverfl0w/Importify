namespace Importify.Application.Common;

/// <summary>
/// Represents the result of an import operation, including the total number of items processed,
/// the number of successful imports, and the number of failed imports.
/// </summary>
public record ImportResult(
    int Total,
    int Success,
    int Failed,
    List<RowError> Errors
)
{
    public bool HasErrors => Errors.Count > 0;
}