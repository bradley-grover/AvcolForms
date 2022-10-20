using AvcolForms.Core.Notifications;

namespace AvcolForms.Web.Services;

/// <summary>
/// Fetches notifications for the currently signed in user to view
/// </summary>
public sealed class NotificationService : INotificationService
{
    /// <inheritdoc></inheritdoc>
    public ValueTask<UserAlert[]> GetUserNotificationsAsync()
    {
        return ValueTask.FromResult(Array.Empty<UserAlert>());
    }
}
