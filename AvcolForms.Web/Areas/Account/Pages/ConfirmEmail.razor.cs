namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Guard page for users who have not confirmed their email
/// </summary>
[Route(Routes.Accounts.ConfirmEmailPage)]
public partial class ConfirmEmail
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Confirm", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };
}
