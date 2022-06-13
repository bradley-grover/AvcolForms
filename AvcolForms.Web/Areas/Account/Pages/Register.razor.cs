using AvcolForms.Core.Components.Dialogs;


namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Register class for the page
/// </summary>
public partial class Register
{
#nullable disable
    [Inject]
    private UserManager<IdentityUser> UserManager { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    [Inject]
    private IDialogService Dialog { get; set; }
#nullable restore



    RegisterAccount Registration { get; } = new();

    bool dialogIsAlreadyOpen = false;
    bool acceptedLicense;
    bool success;

    void GoToLogin()
    {
        NavManager.NavigateTo("/account/login");
    }

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Account", href: null, disabled: true, Icons.Material.Filled.AccountCircle),
        new("Register", href: null, disabled: true, Icons.Material.Filled.AppRegistration)
    };

    async Task OpenDialogAsync()
    {
        if (dialogIsAlreadyOpen)
        {
            return;
        }

        dialogIsAlreadyOpen = true;

        var result = await Dialog.Show<PrivacyDialog>("Privacy Policy").Result;

        if (!result.Cancelled)
        {
            acceptedLicense = (bool)(result.Data ?? false);
        }

        dialogIsAlreadyOpen = false;
    }

    private async Task RegisterAsync()
    {
        var result = await UserManager.CreateAsync(new IdentityUser { UserName = Registration.Email, Email = Registration.Email, EmailConfirmed = true }, Registration.Password);

        if (result.Succeeded)
        {
            NavManager.NavigateTo("account/login");
            return;
        }
    }
}
