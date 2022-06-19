namespace AvcolForms.Web.Areas.Admin;

/// <summary>
/// The admin home page for users with high level permission 
/// </summary>
public partial class AdminPanel
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Admin", href: null, disabled: true, icon: Icons.Material.Filled.AdminPanelSettings),
    };
}
