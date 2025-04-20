namespace Importify.Application.Abstractions;

/// <summary>
/// Defines a service for importing employee data from a CSV stream.
/// </summary>
public interface IEmployeeImporterService
{
    /// <summary>
    /// Imports employee data from the provided CSV stream.
    /// </summary>
    /// <param name="csvStream">The stream containing the CSV data to import.</param>
    /// <param name="createdByUserId">The ID of the user who initiated the import.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the import.</returns>
    Task<ImportResult> ImportAsync(Stream csvStream, Guid createdByUserId);
}