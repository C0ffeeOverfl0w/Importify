namespace Importify.Domain.Abstractions
{
    /// <summary>
    /// Defines the contract for a repository that manages Employee entities.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the employee if found; otherwise, null.
        /// </returns>
        Task<Employee?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of all employees.
        /// </returns>
        Task<IEnumerable<Employee>> GetAllAsync();

        /// <summary>
        /// Adds a new employee to the repository.
        /// </summary>
        /// <param name="employee">The employee to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Employee employee);

        /// <summary>
        /// Updates an existing employee in the repository.
        /// </summary>
        /// <param name="employee">The employee with updated information.</param>
        void Update(Employee employee);

        /// <summary>
        /// Removes an employee from the repository.
        /// </summary>
        /// <param name="employee">The employee to remove.</param>
        void Remove(Employee employee);

        /// <summary>
        /// Retrieves all employees sorted by their full name.
        /// </summary>
        /// <returns>Sorted List</returns>
        Task<List<Employee>> GetAllSortedAsync();

        /// <summary>
        /// Checks if the provided payroll numbers already exist in the repository.
        /// </summary>
        /// <param name="payrollNumbers">PRNumbers</param>
        /// <returns></returns>
        Task<HashSet<string>> GetExistingPayrollNumbersAsync(IEnumerable<string> payrollNumbers);
    }
}