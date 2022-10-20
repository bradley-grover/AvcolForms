using System.Runtime.InteropServices;

namespace AvcolForms.Core.Notifications;

/// <summary>
/// A user alert to be displayed
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct UserAlert
{
    internal readonly AlertSeverity _severity;
    internal readonly string _userMessage;

    /// <summary>
    /// The severity of the alert
    /// </summary>
    public AlertSeverity Severity
    {
        get => _severity;
    }

    /// <summary>
    /// The message to be displayed in the notification itself
    /// </summary>
    public string Message
    {
        get => _userMessage;
    }
}
