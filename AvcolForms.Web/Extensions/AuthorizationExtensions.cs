using Microsoft.AspNetCore.Components.Authorization;

namespace AvcolForms.Web.Extensions;

public static class AuthorizationExtensions
{
    public static async Task<TIdentityUser?> GetIdentityUserAsync<TIdentityUser>(this AuthenticationStateProvider provider, UserManager<TIdentityUser> userManager) 
        where TIdentityUser : IdentityUser
    {
        ArgumentNullException.ThrowIfNull(userManager);

        var state = await provider.GetAuthenticationStateAsync();

        return await userManager.GetUserAsync(state.User);
    }
}
