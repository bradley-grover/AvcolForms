using Microsoft.AspNetCore.Authorization;

namespace AvcolForms.Core.Accounts;

/// <summary>
/// Allows authorization of multiple roles
/// </summary>
public sealed class AuthorizeMultipleAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeMultipleAttribute"/> class with the specified roles
    /// </summary>
    /// <param name="roles">The roles to supply to the attribute</param>
    public AuthorizeMultipleAttribute(params string[] roles)
    {
        Roles = string.Join(',', roles);
    }
}
