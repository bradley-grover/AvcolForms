using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

/// <summary>
/// Register account model, what the user enters to register
/// </summary>
public class RegisterAccount
{
    /// <summary>
    /// Email address of the user
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Password of the user for them to sign in with
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Confirmation field to make the user hasn't incorrectly entered their password
    /// </summary>
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
