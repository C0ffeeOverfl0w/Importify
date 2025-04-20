namespace Importify.Application.Utils;

/// <summary>
/// Factory class for creating pre-configured instances of <see cref="CsvConfiguration"/>.
/// </summary>
public static class CsvConfigurationFactory
{
    /// <summary>
    /// Gets the default CSV configuration with invariant culture, comma as a delimiter,
    /// and no validation for headers or missing fields.
    /// </summary>
    public static CsvConfiguration Default => new(CultureInfo.InvariantCulture)
    {
        Delimiter = ",",
        HeaderValidated = null,
        MissingFieldFound = null
    };
}