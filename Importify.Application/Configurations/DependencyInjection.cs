namespace Importify.Application.Configurations;

/// <summary>
/// Provides extension methods for configuring services in the application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeImporterService, EmployeeImporterService>();
        services.AddScoped<IFileValidatorService, FileValidatorService>();
        return services;
    }
}