using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

public class RegisterAccount
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
