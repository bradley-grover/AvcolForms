using AvcolForms.Web.Areas.Account.ViewModels;

namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page for resetting password after the account password has been forgotten
/// </summary>
[Route(Routes.Accounts.ResetAccountPassword)]
public partial class ResetPassword
{
    [Parameter]
    public string? Code { get; set; }

    /// <summary>
    /// Represents the password model
    /// </summary>
    public ChangePasswordViewModel PasswordModel = new();


    private readonly List<BreadcrumbItem> items = new()
    {
        new("Account", href: null, disabled: true, Icons.Material.Filled.AccountCircle),
        new("Reset Password", href: null, disabled: true, Icons.Material.Filled.Password)
    };
}
