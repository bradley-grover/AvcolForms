using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace AvcolForms.Web.Areas.Account.Controllers;

/// <summary>
/// Controls password related actions of user accounts
/// </summary>
public class PasswordController : Controller
{
    private IDataProtector Protector { get; }
    private UserManager<ApplicationUser> UserManager { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordController"/> class
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="userManager"></param>
    public PasswordController(IDataProtectionProvider provider, UserManager<ApplicationUser> userManager)
    {
        Protector = provider.CreateProtector(Protected.ForgotPassword);
        UserManager = userManager;
    }

    /// <summary>
    /// Sends a password reset request page to the user for them to change their password
    /// </summary>
    /// <param name="t"></param>
    /// <returns>A redirect</returns>
    [HttpGet(Routes.Accounts.ChangeForgotPassword)]
    public async Task<IActionResult> AuthenticatePasswordResetAsync(string t)
    {
        if (string.IsNullOrWhiteSpace(t))
        {
            return BadRequest();
        }

        var data = Protector.Unprotect(t);

        var parts = data.Split('|');

        var user = await UserManager.FindByIdAsync(parts[0]);

        if (!(parts.Length >= 2))
        {
            return BadRequest();
        }

        if (user is null)
        {
            return Unauthorized();
        }

        return Redirect($"{Routes.Accounts.ResetAccountPassword}/{parts[1]}");
    }
}
