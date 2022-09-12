/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

/*
 * The code in this class sets up our dbcontext and selects a provider.
 * Some things to note about what this setup does
 * - This extension method setups up the database so that we spawn instance of DbContextFactory as well
 *   because we want to manage the lifetime and disposal of our context manually because of the way Blazor server works
 * - This code works differently in release build to debug:
 *    In debug mode compilation: 
 *      The method will use an in memory tables to act as the database and will enable detailed errors and sensitive logging of data to the logger
 *    In release mode:
 *      The method will use any of the selected real database providers read from the configuration and load the connection string for the database.
 *      If the provider is not found then it will throw an exception on startup
 * This code also adds the entity framework stores for our authentication of the app and adds account settings
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
#if DEBUG
            options.UseInMemoryDatabase("AvcolForms")
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
#else

            switch (configuration.GetValue<string>(Providers.DbProviderKey))
            {
                case Providers.SqlServerProvider:
                    options.UseSqlServer(configuration.GetConnectionString(Providers.SqlServerProvider), 
                        x => x.MigrationsAssembly(Providers.SqlServerAssembly));
                    break;
                case Providers.SqliteProvider:
                    options.UseSqlite(configuration.GetConnectionString(Providers.SqliteProvider),
                        x => x.MigrationsAssembly(Providers.SqliteAssembly));
                    break;
                case Providers.PostgresProvider:
                    options.UseNpgsql(configuration.GetConnectionString(Providers.PostgresProvider), 
                        x => x.MigrationsAssembly(Providers.PostgresAssembly));
                    break;
                default:
                    throw new InvalidOperationException($"{configuration.GetValue<string>(Providers.DbProviderKey)} is not a valid provider, check documentation for valid ones");
            }
#endif
        });


        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;

            options.SignIn.RequireConfirmedEmail = true;

        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
