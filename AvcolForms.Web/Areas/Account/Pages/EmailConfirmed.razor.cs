namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page to be displayed when the email has been confirmed
/// </summary>
[Route(Routes.Accounts.EmailConfirmedPage)]
public partial class EmailConfirmed
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Confirmed", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };
}
