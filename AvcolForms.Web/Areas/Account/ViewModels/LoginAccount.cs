using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

public class LoginAccount
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
