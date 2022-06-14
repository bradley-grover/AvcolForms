using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Core.Accounts;

#nullable disable

/// <summary>
/// Settings to add a admin user to the debug database for easier testing
/// </summary>
public class SudoSettings
{
    /// <summary>
    /// The email for the account
    /// </summary>
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Password to sign up the account
    /// </summary>
    public string Password { get; set; }
}
