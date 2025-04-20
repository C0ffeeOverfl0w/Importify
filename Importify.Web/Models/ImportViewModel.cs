namespace Importify.Web.Models;

/// <summary>
/// ViewModel representing the result of an import operation and the list of employees involved.
/// </summary>
public class ImportViewModel
{
    /// <summary>
    /// Gets or sets the result of the import operation, including totals and errors.
    /// </summary>
    public ImportResult ImportResult { get; set; } = new(0, 0, 0, new());

    /// <summary>
    /// Gets or sets the list of employees involved in the import operation.
    /// </summary>
    public List<EmployeeDto> Employees { get; set; } = new();
}