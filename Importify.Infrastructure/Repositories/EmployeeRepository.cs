namespace Importify.Infrastructure.Repositories;

/// <summary>
/// Repository for managing employee-related data operations.
/// </summary>
/// <param name="context">DB Context</param>
public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc/>
    public async Task<Employee?> GetByIdAsync(Guid id) =>
        await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<Employee>> GetAllAsync() =>
        await _context.Employees.ToListAsync();

    /// <inheritdoc/>
    public async Task AddAsync(Employee employee) =>
        await _context.Employees.AddAsync(employee);

    /// <inheritdoc/>
    public void Update(Employee employee) =>
        _context.Employees.Update(employee);

    /// <inheritdoc/>
    public void Remove(Employee employee) =>
        _context.Employees.Remove(employee);

    /// <inheritdoc/>
    public async Task<List<Employee>> GetAllSortedAsync()
    {
        return await _context.Employees
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<HashSet<string>> GetExistingPayrollNumbersAsync(IEnumerable<string> payrollNumbers)
    {
        var normalized = payrollNumbers
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct()
            .ToList();

        return [.. (await _context.Employees
                .Where(e => normalized.Contains(e.PayrollNumber))
                .Select(e => e.PayrollNumber)
                .ToListAsync())];
    }
}