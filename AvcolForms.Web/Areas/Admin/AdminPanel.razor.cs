namespace AvcolForms.Web.Areas.Admin;

/// <summary>
/// The admin home page for users with high level permission 
/// </summary>
[Route(Routes.Admin.Dash)]
public partial class AdminPanel
{
#nullable disable
    [Inject]
    private NavigationManager NavManager { get; set; }

    [Inject]
    private IDbContextFactory<ApplicationDbContext> Factory { get; set; }

    private bool isLoading = false;
    private int userCount;

#nullable restore

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: Routes.Home, disabled: false, Icons.Material.Filled.Home),
        new("Admin", href: Routes.Admin.Dash, disabled: true, icon: Icons.Material.Filled.AdminPanelSettings),
    };

    private void GoToUsersPage()
    {
        NavManager.NavigateTo(Routes.Admin.Users);
    }

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;

        try
        {
            using var context = await Factory.CreateDbContextAsync();
            userCount = await context.Users.CountAsync();
        }
        finally
        {
            isLoading = false;
        }
    }
}
