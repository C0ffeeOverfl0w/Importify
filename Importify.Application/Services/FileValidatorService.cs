namespace Importify.Application.Services;

/// <inheritdoc/>
public class FileValidatorService(ILogger<FileValidatorService> logger) : IFileValidatorService
{
    /// <inheritdoc/>
    public bool IsInvalid(IFormFile? file, out IActionResult? errorResult)
    {
        if (file == null || file.Length == 0)
        {
            logger.LogWarning("Invalid file upload attempt.");
            errorResult = new BadRequestObjectResult("File is required.");
            return true;
        }

        if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            errorResult = new BadRequestObjectResult("Only CSV files are allowed.");
            return true;
        }

        errorResult = null;
        return false;
    }
}