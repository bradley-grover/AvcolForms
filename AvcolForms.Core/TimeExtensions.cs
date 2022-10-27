namespace AvcolForms.Core;

/// <summary>
/// Extensions for <see cref="DateTime"/>, <see cref="DateTimeOffset"/> and <see cref="TimeSpan"/>
/// </summary>
public static class TimeExtensions
{
    /// <summary>
    /// Gets the local time or displays an empty value
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToLocalTimeOrNone(this DateTimeOffset? time)
    {
        if (time == null)
        {
            return "None";
        }

        return time.Value.ToLocalTime().ToString();
    }
}
