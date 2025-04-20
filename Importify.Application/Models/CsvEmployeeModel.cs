namespace Importify.Application.Models;

/// <summary>
/// Represents an employee record imported from a CSV file.
/// </summary>
public class CsvEmployeeModel
{
    /// <summary>
    /// Gets or sets the payroll number of the employee.
    /// </summary>
    public string PayrollNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the forenames of the employee.
    /// </summary>
    public string Forenames { get; set; } = default!;

    /// <summary>
    /// Gets or sets the surname of the employee.
    /// </summary>
    public string Surname { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date of birth of the employee.
    /// </summary>
    public string DateOfBirth { get; set; } = default!;

    /// <summary>
    /// Gets or sets the telephone number of the employee.
    /// </summary>
    public string Telephone { get; set; } = default!;

    /// <summary>
    /// Gets or sets the mobile number of the employee.
    /// </summary>
    public string Mobile { get; set; } = default!;

    /// <summary>
    /// Gets or sets the primary address of the employee.
    /// </summary>
    public string Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the secondary address of the employee.
    /// </summary>
    public string Address2 { get; set; } = default!;

    /// <summary>
    /// Gets or sets the postcode of the employee's address.
    /// </summary>
    public string Postcode { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the employee.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the start date of the employee's employment.
    /// </summary>
    public string StartDate { get; set; } = default!;
}