/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Web.ServiceConfigures;

/// <summary>
/// Internal class to add dbcontext to the application
/// </summary>
internal static class DatabaseServiceExtensions
{
    /// <summary>
    /// Adds the DbContext to the <see cref="IServiceCollection"/> with the specifed <see cref="IConfiguration"/>
    /// </summary>
    /// <param name="services">The service container to add the database services to</param>
    /// <param name="configuration">The configuration for the connection string and for the database provider</param>
    /// <returns>The same <see cref="IServiceCollection"/> for chaining</returns>
    /// <exception cref="InvalidOperationException">Throws if we use a not recognized EF provider</exception>
    internal static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("Default");
#if DEBUG
            options.UseInMemoryDatabase("AvcolForms");
#else
            switch (configuration.GetValue<string>("Db-Provider").ToUpper())
            {
                case "SQLSERVER":
                    options.UseSqlServer(connectionString);
                    break;
                case "SQLITE":
                    options.UseSqlite(connectionString);
                    break;
                case "POSTGRES":
                    options.UseNpgsql(connectionString);
                    break;
                default:
                    throw new InvalidOperationException($"{configuration.GetValue<string>("Db-Provider")} is not a valid provider, check documentation for valid ones");
            }
#endif
        });
        return services;
    }
}
