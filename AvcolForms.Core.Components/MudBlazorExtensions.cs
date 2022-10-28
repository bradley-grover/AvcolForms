using AvcolForms.Core.Notifications;

namespace AvcolForms.Core.Components;

public static class MudBlazorExtensions
{
    public static Severity UserAlertSeverityToMud(this AlertSeverity severity)
    {
        return severity switch
        {
            AlertSeverity.Info => Severity.Info,
            AlertSeverity.Urgent => Severity.Warning,
            _ => throw new InvalidOperationException(),// shouldn't reach here
        };
    }
}
