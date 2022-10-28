namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page the user gets sent to after they request to reset their password
/// </summary>
[Route(Routes.Accounts.PasswordResetRequested)]
public partial class SentResetRequest
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Password Reset Sent", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };
}
