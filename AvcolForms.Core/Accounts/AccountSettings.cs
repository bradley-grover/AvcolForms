using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Core.Accounts;

/// <summary>
/// Account settings for the application
/// </summary>
public class AccountSettings
{
    /// <summary>
    /// The length that a password must be for an account
    /// </summary>
    public int PasswordLength { get; set; }
}
