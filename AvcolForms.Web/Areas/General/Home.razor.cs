using AvcolForms.Core.Notifications;

namespace AvcolForms.Web.Areas.General;

/// <summary>
/// Represents the home page
/// </summary>
public partial class Home
{
    private bool _fetchedNotifications = false;
    private List<UserAlert> _alerts = null!;

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: true, Icons.Material.Filled.Home),
    };

    protected override async Task OnInitializedAsync()
    {
        await authenticationStateProvider.GetAuthenticationStateAsync();

        _alerts = (await _notificationService.GetUserNotificationsAsync()).ToList();
        _fetchedNotifications = true;
    }
}
