using Microsoft.AspNetCore.Identity;

namespace AvcolForms.Core.Notifications;

/// <summary>
/// Represents a service to fetch notifications
/// </summary>
public interface INotificationService<TUser>
    where TUser : IdentityUser
{
    /// <summary>
    /// Fetches notifications for a user
    /// </summary>
    /// <param name="user">The user notifications to check for</param>
    /// <returns>The notifications</returns>
    ValueTask<UserAlert[]> GetUserNotificationsAsync(TUser user);
}
