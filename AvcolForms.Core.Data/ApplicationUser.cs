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
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// The last time the user logged into the account
    /// </summary>
    [JsonPropertyName("lastLogin")]
    public DateTimeOffset LastLogin { get; set; }
}
