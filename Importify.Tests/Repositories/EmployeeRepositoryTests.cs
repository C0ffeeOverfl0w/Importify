namespace Importify.Tests.Repositories;

/// <summary>
/// Contains unit tests for the <see cref="EmployeeRepository"/> class,
/// ensuring its methods function as expected.
/// </summary>
public class EmployeeRepositoryTests
{
    /// <summary>
    /// Creates a new in-memory instance of <see cref="ApplicationDbContext"/> for testing purposes.
    /// </summary>
    /// <returns>A new instance of <see cref="ApplicationDbContext"/>.</returns>
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    /// <summary>
    /// Tests that <see cref="EmployeeRepository.GetExistingPayrollNumbersAsync"/>
    /// returns only the payroll numbers that exist in the database.
    /// </summary>
    [Fact]
    public async Task GetExistingPayrollNumbersAsync_ReturnsExisting()
    {
        // Arrange
        using var context = CreateContext();
        var repo = new EmployeeRepository(context);

        var emp1 = CreateEmployee("A001");
        var emp2 = CreateEmployee("B002");

        context.Employees.AddRange(emp1, emp2);
        await context.SaveChangesAsync();

        var input = new[] { "A001", "C003" };

        // Act
        var result = await repo.GetExistingPayrollNumbersAsync(input);

        // Assert
        Assert.Single(result);
        Assert.Contains("A001", result);
    }

    /// <summary>
    /// Tests that <see cref="EmployeeRepository.GetAllSortedAsync"/>
    /// returns employees sorted by creation date in descending order.
    /// </summary>
    [Fact]
    public async Task GetAllSortedAsync_ReturnsNewestFirst()
    {
        // Arrange
        using var context = CreateContext();
        var repo = new EmployeeRepository(context);

        var older = CreateEmployee("A001", createdAt: DateTime.UtcNow.AddHours(-1));
        var newer = CreateEmployee("B002", createdAt: DateTime.UtcNow);

        context.Employees.AddRange(older, newer);
        await context.SaveChangesAsync();

        // Act
        var sorted = await repo.GetAllSortedAsync();

        // Assert
        Assert.Equal("B002", sorted.First().PayrollNumber);
        Assert.Equal("A001", sorted.Last().PayrollNumber);
    }

    /// <summary>
    /// Creates a new <see cref="Employee"/> instance with the specified payroll number and optional creation date.
    /// </summary>
    /// <param name="payroll">The payroll number of the employee.</param>
    /// <param name="createdAt">The optional creation date of the employee. Defaults to the current UTC time.</param>
    /// <returns>A new <see cref="Employee"/> instance.</returns>
    private static Employee CreateEmployee(string payroll, DateTime? createdAt = null)
    {
        var emp = Employee.Create(
            payrollNumber: payroll,
            name: FullName.Create("Test", "User"),
            dateOfBirth: DateTime.Parse("1990-01-01"),
            telephone: "123",
            mobile: "456",
            address: Address.Create("Street", "City", "0000"),
            email: Email.Create("test@example.com"),
            startDate: DateTime.UtcNow
        );
        emp.MarkCreated(createdAt ?? DateTime.UtcNow);
        return emp;
    }
}