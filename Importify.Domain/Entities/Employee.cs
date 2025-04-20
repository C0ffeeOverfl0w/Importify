namespace Importify.Domain.Entities;

/// <summary>
/// Represents an employee entity with personal and professional details.
/// </summary>
public class Employee : Entity
{
    public Employee(Guid id) : base(id)
    {
    }

    /// <summary>
    /// Gets the payroll number of the employee.
    /// </summary>
    public string PayrollNumber { get; private set; }

    /// <summary>
    /// Gets the full name of the employee.
    /// </summary>
    public FullName Name { get; private set; }

    /// <summary>
    /// Gets the date of birth of the employee.
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    /// Gets the telephone number of the employee.
    /// </summary>
    public string Telephone { get; private set; }

    /// <summary>
    /// Gets the mobile number of the employee.
    /// </summary>
    public string Mobile { get; private set; }

    /// <summary>
    /// Gets the address details of the employee.
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    /// Gets the home email address of the employee.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the start date of the employee.
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    /// Gets the date and time when the employee was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Marks the employee as created at the specified timestamp.
    /// </summary>
    /// <param name="timestamp"></param>

    public void MarkCreated(DateTime timestamp) => CreatedAt = timestamp;

    /// <summary>
    /// Initializes a new instance of the <see cref="Employee"/> class with the specified details.
    /// </summary>
    private Employee(
        Guid id,
        string payrollNumber,
        FullName name,
        DateTime dateOfBirth,
        string telephone,
        string mobile,
        Address address,
        Email email,
        DateTime startDate) : base(id)
    {
        Id = id;
        PayrollNumber = payrollNumber;
        Name = name;
        DateOfBirth = dateOfBirth;
        Telephone = telephone;
        Mobile = mobile;
        Address = address;
        Email = email;
        StartDate = startDate;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Employee"/> class.
    /// </summary>
    /// <param name="payrollNumber">The payroll number of the employee.</param>
    /// <param name="name">The full name of the employee.</param>
    /// <param name="dateOfBirth">The date of birth of the employee.</param>
    /// <param name="telephone">The telephone number of the employee.</param>
    /// <param name="mobile">The mobile number of the employee.</param>
    /// <param name="address">The address details of the employee.</param>
    /// <param name="email">The email address of the employee.</param>
    /// <param name="startDate">The start date of the employee.</param>
    /// <returns>A new <see cref="Employee"/> instance.</returns>
    public static Employee Create(
        string payrollNumber,
        FullName name,
        DateTime dateOfBirth,
        string telephone,
        string mobile,
        Address address,
        Email email,
        DateTime startDate)
    {
        return new Employee(
            Guid.NewGuid(),
            payrollNumber,
            name,
            dateOfBirth,
            telephone,
            mobile,
            address,
            email,
            startDate);
    }
}