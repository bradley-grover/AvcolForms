namespace AvcolForms.Web.Areas.Account.Pages;

public partial class SentConfirmation
{
#nullable disable
    [Parameter]
    public string Email { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }
#nullable restore


    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Confirm Your Email", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };

    public void GoToResend()
    {
        NavManager.NavigateTo(AccountRoutes.ResendConfirmation);
    }
}
