using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AvcolForms.Web.Areas.Account.Controllers;

public class AccountController : Controller
{
    private IDataProtector Protector { get; }
    public UserManager<IdentityUser> UserManager { get; }
    public SignInManager<IdentityUser> SignInManager { get; }

    public AccountController(IDataProtectionProvider dataProtectionProvider, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        Protector = dataProtectionProvider.CreateProtector("Login");
    }
    [HttpGet("account/authenticate_login")]
    public async Task<IActionResult> AuthenticateLoginAsync(string t)
    {
        var data = Protector.Unprotect(t);

        var parts = data.Split('|');

        var user = await UserManager.FindByIdAsync(parts[0]);

        var isTokenValid = await UserManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "Login", parts[1]);

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

    [Authorize]
    [HttpGet("account/logout")]
    public async Task<IActionResult> LogOutAsync()
    {
        await SignInManager.SignOutAsync();

        return Redirect("/account/logged_out");
    }
}
