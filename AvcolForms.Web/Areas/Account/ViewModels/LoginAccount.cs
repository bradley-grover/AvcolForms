using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

public class LoginAccount
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
