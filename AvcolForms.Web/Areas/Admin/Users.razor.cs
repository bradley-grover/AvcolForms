namespace AvcolForms.Web.Areas.Admin;

/// <summary>
/// Page to manage users in administrative mode
/// </summary>
public partial class Users
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: Routes.Home, disabled: false, Icons.Material.Filled.Home),
        new("Admin", href: Routes.Admin.Dash, disabled: false, icon: Icons.Material.Filled.AdminPanelSettings),
        new("Manage Users", href: null, disabled: true, icon: Icons.Material.Filled.ManageAccounts)
    };
}
