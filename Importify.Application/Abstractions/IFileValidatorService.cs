namespace Importify.Application.Abstractions;

/// <summary>
/// Provides methods to validate file uploads and
/// return appropriate error results if validation fails.
/// </summary>
public interface IFileValidatorService
{
    /// <summary>
    /// Validates the provided file and determines if it is invalid.
    /// </summary>
    /// <param name="file">The file to validate.</param>
    /// <param name="errorResult">
    /// An output parameter that contains the error result
    /// if the file is invalid; otherwise, null.
    /// </param>
    /// <returns>
    /// True if the file is invalid; otherwise, false.
    /// </returns>
    bool IsInvalid(IFormFile? file, out IActionResult? errorResult);
}