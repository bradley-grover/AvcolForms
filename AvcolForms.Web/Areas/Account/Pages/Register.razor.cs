using System.Text;
using System.Text.Encodings.Web;
using AvcolForms.Core.Components.Dialogs;
using AvcolForms.Web.Areas.Account.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Register class for the page
/// </summary>
public partial class Register
{
#nullable disable
    [Inject]
    private UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    [Inject]
    private IDialogService Dialog { get; set; }

    [Inject]
    private IDataProtectionProvider ProtectionProvider { get; set; }

    [Inject]
    private IEmailSender EmailSender { get; set; }
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
        var user = new ApplicationUser { UserName = Registration.Email, Email = Registration.Email };

        var result = await UserManager.CreateAsync(user, Registration.Password);

        if (result.Succeeded)
        {
            var userId = await UserManager.GetUserIdAsync(user);

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            var protecter = ProtectionProvider.CreateProtector(Protected.ConfirmEmail);

            string value = $"{userId}|{code}";

            value = protecter.Protect(value);

            Uri uri = NavManager.ToAbsoluteUri($"{AccountRoutes.EmailConfirmGet}?t={value}");

            await EmailSender.SendEmailAsync(Registration.Email, "Confirm Your Account",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(uri.ToString())}'>clicking here</a>.");

            var email = Convert.ToBase64String(Encoding.UTF8.GetBytes(Registration.Email));

            NavManager.NavigateTo($"/account/sent_confirmation/{email}", forceLoad: true);

            return;
        }
    }
}
