namespace Importify.Web.Controllers;

/// <summary>
/// Controller responsible for managing employee-related operations,
/// including importing and displaying employees.
/// </summary>
[Route("/")]
public class EmployeesController(
    IEmployeeImporterService importer,
    IUnitOfWork unitOfWork,
    IFileValidatorService fileValidator,
    ILogger<EmployeesController> logger
) : Controller
{
    private const string ImportResultKey = "ImportResult";

    /// <summary>
    /// Handles the import of employee data from a CSV file.
    /// </summary>
    /// <param name="file">The uploaded CSV file containing employee data.</param>
    /// <returns>An IActionResult indicating the result of the import operation.</returns>
    [HttpPost("import")]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (fileValidator.IsInvalid(file, out var errorResult))
            return errorResult!;

        try
        {
            using var stream = file.OpenReadStream();
            var result = await importer.ImportAsync(stream, Guid.Empty);

            if (result.Total > 0)
                TempData["ImportResult"] = JsonSerializer.Serialize(result);

            return RedirectToAction("Index");
        }
        catch (IOException ioEx)
        {
            logger.LogError(ioEx, $"File read error: {ioEx.Message}");
            TempData["ImportError"] = "File read error. Please try again.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during import");
            TempData["ImportError"] = "Error during import";
            return RedirectToAction("Index");
        }
    }

    /// <summary>
    /// Displays the list of employees and the result of the last import operation.
    /// </summary>
    /// <returns>A view containing the list of employees and import results.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employees = await unitOfWork.Employees.GetAllSortedAsync();

        var importResultJson = TempData[ImportResultKey] as string;
        var result = TryDeserialize<ImportResult>(importResultJson)
                     ?? new ImportResult(0, 0, 0, new());

        var viewModel = new ImportViewModel
        {
            ImportResult = result,
            Employees = employees.Select(EmployeeDto.From).ToList()
        };

        return View(viewModel);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string into an object of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize into.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized object, or default if deserialization fails.</returns>
    private static T? TryDeserialize<T>(string? json)
    {
        try
        {
            return string.IsNullOrWhiteSpace(json)
                ? default
                : JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }
}