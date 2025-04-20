namespace Importify.Infrastructure.Configurations;

/// <summary>
/// Provides extension methods for configuring services in the application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration containing the connection string.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
        //        sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServerLocalDb"),
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}