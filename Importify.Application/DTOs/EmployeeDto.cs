using Importify.Domain.Entities;

namespace Importify.Application.DTOs;

/// <summary>
/// Data Transfer Object representing an employee with essential details.
/// </summary>
public record EmployeeDto(
    Guid Id,
    string PayrollNumber,
    string FirstName,
    string Surname,
    string Email,
    string Telephone,
    string Mobile,
    string Address,
    DateTime DateOfBirth,
    DateTime StartDate
)
{
    /// <summary>
    /// Creates an instance of <see cref="EmployeeDto"/> from an <see cref="Employee"/> entity.
    /// </summary>
    /// <param name="e">The employee entity to map from.</param>
    /// <returns>A new <see cref="EmployeeDto"/> instance with mapped properties.</returns>
    public static EmployeeDto From(Employee e) => new(
        e.Id,
        e.PayrollNumber,
        e.Name.FirstName,
        e.Name.LastName,
        e.Email.Value,
        e.Telephone,
        e.Mobile,
        e.Address.ToString(),
        e.DateOfBirth,
        e.StartDate
    );
}