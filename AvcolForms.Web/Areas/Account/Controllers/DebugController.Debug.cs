using AvcolForms.Core.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AvcolForms.Web.Areas.Account.Controllers;

/// <summary>
/// Controller for debug only routes to be accessed from. This file is not compiled unless the preprocessor symbol 'DEBUG' is defined in the compilation process
/// </summary>
public sealed class DebugController : Controller
{
    private UserManager<ApplicationUser> UserManager { get; }
    private SignInManager<ApplicationUser> SignInManager { get; }
    private IOptions<RootUserOptions> Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DebugController"/> class
    /// </summary>
    /// <param name="userManager">User manager to interact with users in the databse</param>
    /// <param name="signInManager"></param>
    /// <param name="options"></param>
    public DebugController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<RootUserOptions> options)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        Options = options;
    }


    /// <summary>
    /// Allows for the root user to sign in with a single click during debug mode to speed up development time
    /// </summary>
    /// <returns>A redirect after signing in</returns>
    [HttpGet(Routes.Debug.SignIn)]
    [AllowAnonymous]
    public async Task<IActionResult> SignInAsync()
    {
        var user = await UserManager.FindByEmailAsync(Options.Value.Email).ConfigureAwait(false);

        await SignInManager.SignInAsync(user, true);

        return Redirect("/");
    }
}
