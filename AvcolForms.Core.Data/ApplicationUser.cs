using Microsoft.AspNetCore.Identity;

namespace AvcolForms.Core.Data;

/// <summary>
/// The application user for the application
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// The datetime object representing when the user account was created and added to the database on
    /// </summary>
    public DateTimeOffset Created { get; set; }
}
