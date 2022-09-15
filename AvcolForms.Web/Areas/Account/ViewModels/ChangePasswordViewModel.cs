using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

/// <summary>
/// Input for when a user needs to create a new password because they forgot their old one
/// </summary>
public class ChangePasswordViewModel
{
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
