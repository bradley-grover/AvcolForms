using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Core.Accounts;

#nullable disable

/// <summary>
/// The login model to sign into the account
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Email address for the account
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Password to authenticate the sign in
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
