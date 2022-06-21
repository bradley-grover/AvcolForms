using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

public class ForgotPassword
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
}
