namespace Importify.Application.Utils;

/// <summary>
/// Factory class for creating instances of <see cref="RowError"/>.
/// </summary>
public static class RowErrorFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="RowError"/> with the specified details.
    /// </summary>
    /// <param name="lineNumber">The line number in the CSV file where the error occurred.</param>
    /// <param name="message">The error message describing the issue.</param>
    /// <param name="row">The CSV row data associated with the error.</param>
    /// <returns>A new <see cref="RowError"/> instance.</returns>
    public static RowError Create(int lineNumber, string message, CsvEmployeeModel row) => new(lineNumber, message, row);
}