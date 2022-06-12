using AvcolForms.Web.Areas;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AvcolForms.Web.ServiceConfigures;

/// <summary>
/// Provides the static method to configure scoped services in the <see cref="IServiceCollection"/>
/// </summary>
internal static class ScopedConfigures
{
    /// <summary>
    /// Adds lifetime service of scoped to the service collection for dependancy injection
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The same <see cref="IServiceCollection"/> used for chaining</returns>
    internal static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

        return services;
    }
}
