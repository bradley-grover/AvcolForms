using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace AvcolForms.Web.Areas.Account.Controllers;

/// <summary>
/// Accounts controller to manage account related actions
/// </summary>
public class AccountController : Controller
{
    private IDataProtector LoginProtector { get; }
    private IDataProtector EmailConfirmProtector { get; }
    public UserManager<ApplicationUser> UserManager { get; }
    public SignInManager<ApplicationUser> SignInManager { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class
    /// </summary>
    /// <param name="dataProtectionProvider"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public AccountController(IDataProtectionProvider dataProtectionProvider, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        LoginProtector = dataProtectionProvider.CreateProtector(Protected.Login);
        EmailConfirmProtector = dataProtectionProvider.CreateProtector(Protected.ConfirmEmail);
    }

    /// <summary>
    /// Authenticates a login through the login page
    /// </summary>
    /// <param name="t">The data delimited by a character</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="IActionResult"/></returns>
    [HttpGet(Routes.Accounts.AuthenticateLogin)]
    public async Task<IActionResult> AuthenticateLoginAsync(string t)
    {
        var data = LoginProtector.Unprotect(t);

        var parts = data.Split('|');

        var user = await UserManager.FindByIdAsync(parts[0]);

        var isTokenValid = await UserManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, Protected.Login, parts[1]);

        if (isTokenValid)
        {
            await SignInManager.SignInAsync(user, true);

            if (parts.Length == 3 && Url.IsLocalUrl(parts[2]))
            {
                return Redirect(parts[2]);
            }
            return Redirect("/");
        }
        else
        {
            return Unauthorized("Unauthorized request");
        }
    }

    /// <summary>
    /// Authenticates/verifies that the email is actually the users
    /// </summary>
    /// <param name="t">Data delimited by a character</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="IActionResult"/></returns>
    [HttpGet(Routes.Accounts.EmailConfirmGet)]
    public async Task<IActionResult> AuthenticateEmailAsync(string t)
    {
        var data = EmailConfirmProtector.Unprotect(t);

        var parts = data.Split('|');

        var user = await UserManager.FindByIdAsync(parts[0]);

        var confirmResult = await UserManager.ConfirmEmailAsync(user, parts[1]);

        if (!confirmResult.Succeeded)
        {
            return BadRequest("Failed to authenticate the email");
        }

        return Redirect("/account/email_confirmed");
    }

    /// <summary>
    /// Logs out an authenticated user
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="IActionResult"/></returns>
    [Authorize]
    [HttpGet("account/logout")]
    public async Task<IActionResult> LogOutAsync()
    {
        await SignInManager.SignOutAsync();

        return Redirect("/account/logged_out");
    }
}
