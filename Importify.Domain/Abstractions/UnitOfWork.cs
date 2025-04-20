namespace Importify.Domain.Abstractions;

/// <summary>
/// Defines the contract for a unit of work, providing access to repositories
/// and a method to save changes to the underlying data store.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for role-related operations.
    /// </summary>
    IEmployeeRepository Employees { get; }

    /// <summary>
    /// Saves all changes made in the unit of work to the data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the data store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}