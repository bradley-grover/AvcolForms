using AvcolForms.Core.Data.Models;
using AvcolForms.Core.Notifications;

namespace AvcolForms.Web.Services;

/// <summary>
/// Fetches notifications for the currently signed in user to view
/// </summary>
public sealed class NotificationService : INotificationService<ApplicationUser>
{
    internal readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationService"/> class
    /// </summary>
    public NotificationService(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    /// <inheritdoc></inheritdoc>
    public async ValueTask<UserAlert[]> GetUserNotificationsAsync(ApplicationUser user)
    {
        return await _context.Forms
            .Where(f => f.Recipients.Any(u => u == user))
            .Select(f => new UserAlert(AlertSeverity.Info, $"{f.TimeLeft}\n{f.Receiver}\n{f.Title}")).ToArrayAsync();
    }
}
