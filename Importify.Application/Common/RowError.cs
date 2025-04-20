namespace Importify.Application.Common;

/// <summary>
/// Represents an error encountered while processing a specific row in a CSV file.
/// </summary>
/// <param name="LineNumber">The line number in the CSV file where the error occurred.</param>
/// <param name="Message">A description of the error.</param>
/// <param name="Row">The data of the row that caused the error.</param>
public sealed record RowError(int LineNumber, string Message, CsvEmployeeModel Row);