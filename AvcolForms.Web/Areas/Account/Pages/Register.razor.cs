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
[Route(Routes.Accounts.Register)]
public partial class Register
{
#nullable disable
    [Inject]
    private ISnackbar Snackbar { get; set; }
    [Inject]
    private ILogger<Register> Logger { get; set; }

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

    private string? error = null;

    bool registerButtonLock = false;
    bool proccessing = false;

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Account", href: null, disabled: true, Icons.Material.Filled.AccountCircle),
        new("Register", href: null, disabled: true, Icons.Material.Filled.AppRegistration)
    };

    private readonly SensitiveStore PasswordStore = new();
    private readonly SensitiveStore ConfirmStore = new();

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
        registerButtonLock = true;
        proccessing = true;

        var user = new ApplicationUser { UserName = Registration.Email, Email = Registration.Email, Created = DateTimeOffset.UtcNow };

        var result = await UserManager.CreateAsync(user, Registration.Password);

        if (result.Succeeded)
        {
            success = true;
            error = null;

            var userId = await UserManager.GetUserIdAsync(user);

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            var protecter = ProtectionProvider.CreateProtector(Protected.ConfirmEmail);

            string value = $"{userId}|{code}";

            value = protecter.Protect(value);

            Uri uri = NavManager.ToAbsoluteUri($"{Routes.Accounts.EmailConfirmGet}?t={value}");

            try
            {
                await EmailSender.SendEmailAsync(Registration.Email, "Confirm Your Account",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(uri.ToString())}'>clicking here</a>.");
            }
            catch (Exception exception)
            {
                Logger.LogError("{exception}", exception);
                success = false;
                error = $"An error occured whilst trying to send you a confirmation email, resend by <a href='{Routes.Accounts.ResendConfirmation}'>clicking here</a>.";
                proccessing = false;
                return;
            }

            var email = Convert.ToBase64String(Encoding.UTF8.GetBytes(Registration.Email));

            Snackbar.Add("Succesfully Registered!", Severity.Success, c =>
            {
                c.ShowCloseIcon = true;
                c.CloseAfterNavigation = false;
                c.VisibleStateDuration = 10 * 1000;
            });

            user.Created = DateTimeOffset.UtcNow;

            await UserManager.UpdateAsync(user);

            NavManager.NavigateTo($"/account/sent_confirmation/{email}", forceLoad: true);

            proccessing = false;

            return;
        }
        else
        {
            success = false;

            error = string.Join("\n", result.Errors.Select(x => x.Description));

            proccessing = false;
            registerButtonLock = false;
        }
    }
}
