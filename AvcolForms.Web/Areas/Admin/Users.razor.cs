using AvcolForms.Web.Areas.Account.ViewModels;

namespace AvcolForms.Web.Areas.Admin;

/// <summary>
/// Page to manage users in administrative mode
/// </summary>
public partial class Users
{
#nullable disable
    private MudTable<UserViewModel> userTable;
    private readonly List<int> _items = new();
    private string searchString;
    private int pageSize = 10;

    [Inject]
    private IDbContextFactory<ApplicationDbContext> Factory { get; set; }

    private bool Lock { get; set; } = false;
#nullable restore

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: Routes.Home, disabled: false, Icons.Material.Filled.Home),
        new("Admin", href: Routes.Admin.Dash, disabled: false, icon: Icons.Material.Filled.AdminPanelSettings),
        new("Manage Users", href: null, disabled: true, icon: Icons.Material.Filled.ManageAccounts)
    };

    private async Task<TableData<UserViewModel>> GetUsersAsync(TableState state)
    {
        using var context = Factory.CreateDbContext();

        var itemCount = await context.Users.CountAsync();

        var items = await context.Users.Take(pageSize).ToListAsync();

        var data = items.Select(x =>
        {
            return new UserViewModel()
            {
                Email = x.Email,
                EmailIsConfirmed = x.EmailConfirmed
            };
        });

        return new TableData<UserViewModel> { Items = data, TotalItems = itemCount };
    }
}
