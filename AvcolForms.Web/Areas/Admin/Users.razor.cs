using AvcolForms.Web.Areas.Account.ViewModels;

namespace AvcolForms.Web.Areas.Admin;

/// <summary>
/// Page to manage users in administrative mode
/// </summary>
public partial class Users
{
#nullable disable
    private MudTable<UserViewModel> userTable;
    private TableData<UserViewModel> currentData = new();
    private bool isBusy = false;
    private string searchString;
    private bool isAdminstrator;

    [Inject]
    private IDbContextFactory<ApplicationDbContext> Factory { get; set; }

    [Inject]
    private UserManager<ApplicationUser> UserManager { get; set; }

#nullable restore

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: Routes.Home, disabled: false, Icons.Material.Filled.Home),
        new("Admin", href: Routes.Admin.Dash, disabled: false, icon: Icons.Material.Filled.AdminPanelSettings),
        new("Manage Users", href: null, disabled: true, icon: Icons.Material.Filled.ManageAccounts)
    };

    private async Task<TableData<UserViewModel>> GetUsersAsync(TableState state)
    {
        if (isBusy)
        {
            return currentData;
        }

        isBusy = true;

        try
        {
            using var context = Factory.CreateDbContext();

            var itemCount = await context.Users.CountAsync();

            var items = await context.Users.Take(state.PageSize)
                .Select(x => UserViewModel.ConvertToUIModel(x))
                .ToListAsync();

            currentData = new() { Items = items, TotalItems = itemCount };

            return currentData;
        }
        finally
        {
            isBusy = false;
        }
    }
}
