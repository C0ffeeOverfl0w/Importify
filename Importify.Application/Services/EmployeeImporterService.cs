namespace Importify.Application.Services;

/// <summary>
/// Service for importing employee data from a CSV file.
/// </summary>
/// <param name="unitOfWork">UOW</param>
public class EmployeeImporterService(IUnitOfWork unitOfWork, ILogger<EmployeeImporterService> logger) : IEmployeeImporterService
{
    /// <inheritdoc/>
    public async Task<ImportResult> ImportAsync(Stream csvStream, Guid createdByUserId)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CsvConfigurationFactory.Default);

        csv.Context.RegisterClassMap<CsvEmployeeModelMap>();
        var records = csv.GetRecords<CsvEmployeeModel>().ToList();

        var existingPayrollNumbers = await unitOfWork.Employees
            .GetExistingPayrollNumbersAsync(records.Select(r => r.PayrollNumber));

        var seenInFile = new HashSet<string>();
        var errors = new List<RowError>();
        int success = 0, failed = 0;

        foreach (var (row, index) in records.Select((r, i) => (r, i + 2)))
        {
            if (!seenInFile.Add(row.PayrollNumber))
            {
                failed++;
                errors.Add(new RowError(index, $"Duplicate payroll number in file: {row.PayrollNumber}", row));
                continue;
            }

            if (existingPayrollNumbers.Contains(row.PayrollNumber))
            {
                failed++;
                errors.Add(new RowError(index, $"Duplicate payroll number in database: {row.PayrollNumber}", row));
                continue;
            }

            if (TryImportRow(row, out var employee, out var error))
            {
                employee!.MarkCreated(DateTime.UtcNow);
                await unitOfWork.Employees.AddAsync(employee);
                success++;
            }
            else
            {
                failed++;
                logger.LogWarning("Row {Index} failed: {Reason}", index, error);
                errors.Add(new RowError(index, error!, row));
            }
        }

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Imported {Success} out of {Total} employees (Failed: {Failed})",
            success, records.Count, failed);

        return new ImportResult(records.Count, success, failed, errors);
    }

    private static bool TryImportRow(CsvEmployeeModel row, out Employee? employee, out string? error)
    {
        employee = null;
        error = null;

        try
        {
            if (!FullName.TryParse(row.Forenames, row.Surname, out var fullName))
            {
                error = "Invalid name.";
                return false;
            }

            if (!Email.TryParse(row.Email, out var email))
            {
                error = "Invalid email.";
                return false;
            }

            if (!Address.TryParse(row.Address, row.Address2, row.Postcode, out var address))
            {
                error = "Invalid address.";
                return false;
            }

            employee = Employee.Create(
                payrollNumber: row.PayrollNumber,
                name: fullName,
                dateOfBirth: DateTime.Parse(row.DateOfBirth),
                telephone: row.Telephone,
                mobile: row.Mobile,
                address: address,
                email: email,
                startDate: DateTime.Parse(row.StartDate)
            );

            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }
}