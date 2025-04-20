namespace Importify.Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation that provides access to repositories and manages database transactions.
/// </summary>
/// <param name="context">DB Context</param>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc/>
    public IEmployeeRepository Employees { get; } = new EmployeeRepository(context);

    /// <inheritdoc/>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}