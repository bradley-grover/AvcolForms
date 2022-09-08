namespace AvcolForms.Core;

/// <summary>
/// Constants used by the core library for things like lengths
/// </summary>
public static class Constants
{
    /// <summary>
    /// The maximum amount of characters allowed in a email address
    /// </summary>
    public const int MaxEmailLength = 256;

    /// <summary>
    /// The error message for when the email is too long
    /// </summary>
    public const string MaxEmailLengthError = $"The maximum allowed length for an email is 256";
}
