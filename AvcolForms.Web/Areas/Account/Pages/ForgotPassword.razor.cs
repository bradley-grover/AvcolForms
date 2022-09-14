namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page for when you forget your password
/// </summary>
public partial class ForgotPassword
{
#nullable disable
    private string Email { get; set; }
    private bool success;
    private bool buttonLock;
    private bool processing;
#nullable restore

    private string? error = null;

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Login", href: Routes.Accounts.Login, disabled: false, Icons.Material.Filled.Login),
        new("Forgot Password", href: null, disabled: true, icon: Icons.Material.Filled.Password)
    };

    private async Task ProcessAsync()
    {
        
    }
}
