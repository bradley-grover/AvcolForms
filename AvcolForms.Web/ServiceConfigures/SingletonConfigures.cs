using AvcolForms.Core.FileSaving;

namespace AvcolForms.Web.ServiceConfigures;

/// <summary>
/// Provides the static method to configure singletons in the <see cref="IServiceCollection"/>
/// </summary>
internal static class SingletonConfigures
{
    /// <summary>
    /// Adds lifetime service of singletons to the service collection for dependancy injection
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The same <see cref="IServiceCollection"/> used for chaining</returns>
    internal static IServiceCollection AddSingletons(this IServiceCollection services)
    {
        services.AddSingleton<IFileSaver, FileSaver>();

        return services;
    }
}
