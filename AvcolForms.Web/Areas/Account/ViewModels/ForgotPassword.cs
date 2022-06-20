using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Web.Areas.Account.ViewModels;

#nullable disable

public class ForgotPassword
{
    [EmailAddress]
    public string Email { get; set; }
}
