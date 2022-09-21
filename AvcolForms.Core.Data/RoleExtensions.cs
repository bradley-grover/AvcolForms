using System.Diagnostics.Contracts;
using System.Security.Principal;

namespace AvcolForms.Core.Data;

/// <summary>
/// Extensions for roles
/// </summary>
public static class RoleExtensions
{
    /// <summary>
    /// The user is in any of these roles
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    [Pure]
    public static bool IsInAnyRole(this IPrincipal user, ReadOnlySpan<string> roles)
    {
        return roles.Any(user.IsInRole);
    }
}
