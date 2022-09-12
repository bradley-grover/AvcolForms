namespace AvcolForms.Web.Areas.General;

/// <summary>
/// Represents the home page
/// </summary>
public partial class Home
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: true, Icons.Material.Filled.Home),
    };
}
