using System.Web;
using AvcolForms.Core.Accounts;
using Microsoft.AspNetCore.DataProtection;

namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Login page for the user to sign in
/// </summary>
public partial class Login
{
#nullable disable
    [Inject]
    UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    [Inject]
    private IDataProtectionProvider ProtectionProvider { get; set; }
#nullable restore

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Login", href: null, disabled: true, icon: Icons.Material.Filled.Login)
    };

    private bool showLoginError = false;

    private LoginModel SignIn { get; } = new();

    async Task LoginAsync()
    {
        var user = await UserManager.FindByEmailAsync(SignIn.Email);

        if (user is not null && await UserManager.CheckPasswordAsync(user, SignIn.Password))
        {
            showLoginError = false;

            if (!await UserManager.IsEmailConfirmedAsync(user))
            {
                NavManager.NavigateTo(Routes.Accounts.ConfirmEmailPage);
                return;
            }

            var token = await UserManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, Protected.Login);

            var data = $"{user.Id}|{token}|{SignIn.Persist}";

            var parsedQuery = HttpUtility.ParseQueryString(new Uri(NavManager.Uri).Query);

            var returnUrl = parsedQuery["returnUrl"];

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                data += $"|{returnUrl}";
            }

            var protector = ProtectionProvider.CreateProtector(Protected.Login);

            var protectedData = protector.Protect(data);

            NavManager.NavigateTo("/account/authenticate_login?t=" + protectedData, forceLoad: true);
        }
        else
        {
            showLoginError = true;
        }
    }
}
