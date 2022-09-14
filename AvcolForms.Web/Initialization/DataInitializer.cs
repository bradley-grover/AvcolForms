using AvcolForms.Core.Accounts;
using AvcolForms.Core.Options;
using Microsoft.Extensions.Options;

// In this class implementation of IDataInitializor we use the async methods of Entity Framework's RoleManager and UserManager to create and assign roles to users
// The methods are run using sync instead of async as we can't await async methods in a sync call stack without getting a lot of build warnings.

namespace AvcolForms.Web.Initialization;

/// <summary>
/// Used for initiailizing seeed data that needs to run during startup of the application
/// </summary>
public sealed class DataInitializer : IDataInitializor
{
    private RoleManager<IdentityRole> RoleManager { get; }
    private UserManager<ApplicationUser> UserManager { get; }

    private IOptions<SeedAccountOptions> SeededAccounts { get; }
    private IOptions<RootUserOptions> RootUserOptions { get; }

    private ILogger<IDataInitializor> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataInitializer"/> class
    /// </summary>
    /// <param name="provider">A service provider passed in to resolve dependencies as there are a lot</param>
    public DataInitializer(IServiceProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider, nameof(provider));

        RoleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        UserManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        SeededAccounts = provider.GetRequiredService<IOptions<SeedAccountOptions>>();
        RootUserOptions = provider.GetRequiredService<IOptions<RootUserOptions>>();
        Logger = provider.GetRequiredService<ILogger<IDataInitializor>>();
    }

    /// <inheritdoc></inheritdoc>
    public void Initialize()
    {
        bool dbHasAdminRole = Async.RunSync(() => RoleManager.RoleExistsAsync(Roles.Admin));

        // if the admin role doesn't exist in the database we create it

        if (!dbHasAdminRole)
        {
            Async.RunSync(() => RoleManager.CreateAsync(new IdentityRole(Roles.Admin)));
        }


        InitializeMultipleUsers();
        InitializeRootUser();
    }

    private void InitializeRootUser()
    {
        // creates a 'sudo' user that will have full authority over the system

        Logger.LogInformation("R-User: {email} | {passwordLength} | Root", RootUserOptions.Value.Email, new string('*', RootUserOptions.Value.Password.Length));

        var user = Async.RunSync(() => UserManager.FindByEmailAsync(RootUserOptions.Value.Email));

        if (user is not null)
        {
            return;
        }

        ApplicationUser rootAccount = new()
        {
            Email = RootUserOptions.Value.Email,
            UserName = RootUserOptions.Value.Email,
            EmailConfirmed = true
        };

        var result = Async.RunSync(() => UserManager.CreateAsync(rootAccount, RootUserOptions.Value.Password));

        if (result.Succeeded)
        {
            Async.RunSync(() => UserManager.AddToRoleAsync(rootAccount, Roles.Admin));
        }
    }

    private void InitializeMultipleUsers()
    {
        // creates demo users for debug purposes

        if (SeededAccounts.Value.Accounts is null || SeededAccounts.Value.Accounts.Length == 0)
        {
            Logger.LogInformation("There are zero accounts to be added for seeding");
            return;
        }

        foreach (AccountModel account in SeededAccounts.Value.Accounts)
        {
            Logger.LogInformation("S-Account: {email} | {passwordLength} | {role}", account.Email, new string('*', account.Password.Length), account.Role ?? "None");

            var user = Async.RunSync(() => UserManager.FindByEmailAsync(account.Email));

            if (user is not null)
            {
                continue;
            }

            ApplicationUser userAccount = new()
            {
                Email = account.Email,
                UserName = account.Email
            };

            if (account.BypassEmail != null && account.BypassEmail.Value)
            {
                userAccount.EmailConfirmed = true;
            }

            var newUser = Async.RunSync(() => UserManager.CreateAsync(userAccount, account.Password));

            if (newUser.Succeeded)
            {
                if (account.Role is null || !Async.RunSync(() => RoleManager.RoleExistsAsync(account.Role)))
                {
                    continue;
                }

                Async.RunSync(() => UserManager.AddToRoleAsync(userAccount, account.Role));
            }
        }
    }
}
