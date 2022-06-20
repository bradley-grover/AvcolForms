namespace AvcolForms.Web.Areas.Account.Pages;

public partial class EmailConfirmed
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Confirmed", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };
}
