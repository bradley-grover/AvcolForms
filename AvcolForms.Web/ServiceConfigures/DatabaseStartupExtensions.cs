/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace AvcolForms.Web.ServiceConfigures;

/// <summary>
/// Internal class to add dbcontext to the application
/// </summary>
internal static class DatabaseServiceExtensions
{
#if !DEBUG
    private const string SqliteProvider = "Sqlite";
    private const string SqlServerProvider = "SqlServer";
    private const string PostgresProvider = "Postgres";
#endif

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
#if DEBUG
            options.UseInMemoryDatabase("AvcolForms")
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
#else
            string migrationsAssembly = "AvcolForms.Core.Data.";

            switch (configuration.GetValue<string>("Db-Provider"))
            {
                case SqlServerProvider:
                    migrationsAssembly += SqlServerProvider;
                    options.UseSqlServer(configuration.GetConnectionString(SqlServerProvider), 
                        x => x.MigrationsAssembly(migrationsAssembly));
                    break;
                case SqliteProvider:
                    migrationsAssembly += SqliteProvider;
                    options.UseSqlite(configuration.GetConnectionString(SqliteProvider),
                        x => x.MigrationsAssembly(migrationsAssembly));
                    break;
                case PostgresProvider:
                    migrationsAssembly += "Postgres";
                    options.UseNpgsql(configuration.GetConnectionString(PostgresProvider), 
                        x => x.MigrationsAssembly(migrationsAssembly));
                    break;
                default:
                    throw new InvalidOperationException($"{configuration.GetValue<string>("Db-Provider")} is not a valid provider, check documentation for valid ones");
            }
#endif
        });

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}
