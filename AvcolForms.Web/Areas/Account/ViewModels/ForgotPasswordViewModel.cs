using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

/// <summary>
/// View model for a user who has forgot their password
/// </summary>
public class ForgotPasswordViewModel
{
    /// <summary>
    /// Email to send confirmation to
    /// </summary>
    [EmailAddress]
    [Required]
    public string Email { get; set; }
}
