using AvcolForms.Core.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AvcolForms.Web.Areas.Account.Controllers;

public class DebugController : Controller
{
    private UserManager<ApplicationUser> UserManager { get; }
    private SignInManager<ApplicationUser> SignInManager { get; }
    private IOptions<RootUserOptions> Options { get; }

    public DebugController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<RootUserOptions> options)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        Options = options;
    }


    [HttpGet(Routes.Debug.SignIn)]
    [AllowAnonymous]
    public async Task<IActionResult> SignInAsync()
    {
        var user = await UserManager.FindByEmailAsync(Options.Value.Email).ConfigureAwait(false);

        await SignInManager.SignInAsync(user, true);

        return Redirect("/");
    }
}
