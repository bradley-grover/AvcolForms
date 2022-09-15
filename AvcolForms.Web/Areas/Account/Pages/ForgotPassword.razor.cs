using AvcolForms.Web.Areas.Account.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page for when you forget your password
/// </summary>
public partial class ForgotPassword
{
#nullable disable
    [Inject]
    UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    IEmailSender Sender { get; set; }

    [Inject]
    NavigationManager NavManager { get; set; }

    [Inject]
    IDataProtectionProvider ProtectionProvider { get; set; }

    [Inject]
    ILogger<ForgotPassword> Logger { get; set; }
#nullable restore

    private ForgotPasswordViewModel ForgotModel { get; } = new();
    private bool success;  

    bool buttonLock = false;
    bool processing = false;
    private string? message = null;

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Login", href: Routes.Accounts.Login, disabled: false, Icons.Material.Filled.Login),
        new("Forgot Password", href: null, disabled: true, icon: Icons.Material.Filled.Password)
    };

    private async Task ProcessAsync()
    {
        buttonLock = true;
        processing = true;

        var user = await UserManager.FindByEmailAsync(ForgotModel.Email);

        if (user is null || !(await UserManager.IsEmailConfirmedAsync(user)))
        {
            buttonLock = false;
            processing = false;
            return;
        }

        var userId = await UserManager.GetUserIdAsync(user);
        var code = await UserManager.GeneratePasswordResetTokenAsync(user);

        var protector = ProtectionProvider.CreateProtector(Protected.ForgotPassword);

        string value = $"{userId}|{code}";

        value = protector.Protect(value);

        Uri uri = NavManager.ToAbsoluteUri($"{Routes.Accounts.ChangeForgotPassword}?t={value}");

        try
        {
            await Sender.SendEmailAsync(ForgotModel.Email, "Reset Your Password",
                $"Reset your password <a href='{HtmlEncoder.Default.Encode(uri.ToString())}>here</a>'");
        }
        catch (Exception ex)
        {
            Logger.LogError("{exception}", ex);
            success = false;
            processing = false;
            return;
        }
    }
}
