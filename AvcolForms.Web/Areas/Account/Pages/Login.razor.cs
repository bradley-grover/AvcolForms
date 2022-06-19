using Microsoft.AspNetCore.DataProtection;

namespace AvcolForms.Web.Areas.Account.Pages;

public partial class Login
{
#nullable disable
    [Inject]
    UserManager<IdentityUser> UserManager { get; set; }

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

    private LoginAccount SignIn { get; } = new();

    void GoToRegister()
    {
        NavManager.NavigateTo("/account/register");
    }

    async Task LoginAsync()
    {
        var user = await UserManager.FindByEmailAsync(SignIn.Email);

        if (user is not null && await UserManager.CheckPasswordAsync(user, SignIn.Password))
        {
            showLoginError = false;
            var token = await UserManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "Login");

            var data = $"{user.Id}|{token}";

            var parsedQuery = System.Web.HttpUtility.ParseQueryString(new Uri(NavManager.Uri).Query);

            var returnUrl = parsedQuery["returnUrl"];

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                data += $"|{returnUrl}";
            }

            var protector = ProtectionProvider.CreateProtector("Login");

            var protectedData = protector.Protect(data);

            NavManager.NavigateTo("account/authenticate_login?t=" + protectedData, forceLoad: true);
        }
        else
        {
            showLoginError = true;
        }
    }
}
