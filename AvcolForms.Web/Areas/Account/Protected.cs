namespace AvcolForms.Web.Areas.Account;

/// <summary>
/// Contains the protected items used for encoding items
/// </summary>
internal static class Protected
{
    /// <summary>
    /// Protected data for the login
    /// </summary>
    public const string Login = nameof(Login);

    /// <summary>
    /// Protected data for confirming the email
    /// </summary>
    public const string ConfirmEmail = nameof(ConfirmEmail);

    /// <summary>
    /// Protected data for when resetting password
    /// </summary>
    public const string ForgotPassword = nameof(ForgotPassword);
}
