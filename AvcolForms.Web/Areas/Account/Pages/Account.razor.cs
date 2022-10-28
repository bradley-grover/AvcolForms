namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Account page of the app
/// </summary>
[Route(Routes.Accounts.Account)]
public partial class Account
{
    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
    };

    private ApplicationUser? _user;

    protected override async Task OnInitializedAsync()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();

        _user = await _userManager.GetUserAsync(state.User);
    }
}
