namespace AvcolForms.Core.Notifications;

/// <summary>
/// Represents a service to fetch notifications
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Fetches notifications for a specified user
    /// </summary>
    /// <returns>An array of user alerts</returns>
    ValueTask<UserAlert[]> GetUserNotificationsAsync();
}
