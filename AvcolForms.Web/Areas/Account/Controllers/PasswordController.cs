using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace AvcolForms.Web.Areas.Account.Controllers;

public class PasswordController : Controller
{
    private IDataProtector Protector { get; }
    private UserManager<ApplicationUser> UserManager { get; }

    public PasswordController(IDataProtectionProvider provider, UserManager<ApplicationUser> userManager)
    {
        Protector = provider.CreateProtector(Protected.ForgotPassword);
        UserManager = userManager;
    }

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
