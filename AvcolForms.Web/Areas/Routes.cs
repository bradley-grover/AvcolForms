namespace AvcolForms.Web.Areas;

/// <summary>
/// Represents all accessible routes of the application
/// </summary>
internal static class Routes
{
    /// <summary>
    /// The route of the home page
    /// </summary>
    public const string Home = "#";

    /// <summary>
    /// The route for when a error occurs
    /// </summary>
    public const string Error = "/Error";

    /// <summary>
    /// The fallback page for when the client loses connection
    /// </summary>
    public const string Fallback = "/_Host";

    /// <summary>
    /// Routes that only a user with the role of <see cref="Roles.Admin"/> can access
    /// </summary>
    internal static class Admin
    {
        /// <summary>
        /// The base route of the admin routes
        /// </summary>
        internal const string Base = "/admin";
        
        /// <summary>
        /// The dashboard page for admins
        /// </summary>
        internal const string Dash = $"{Base}/dash";

        /// <summary>
        /// The manage users page for the admins
        /// </summary>
        internal const string Users = $"{Base}/users";
    }

    /// <summary>
    /// The accounts routes, for registering, login, signout and any other user related action
    /// </summary>
    internal static class Accounts
    {
        /// <summary>
        /// Standard account page to display information to the user about their account
        /// </summary>
        public const string Account = "/account";

        public const string AuthenticateLogin = $"{Account}/authenticate_login";
        public const string Login = $"{Account}/login";
        public const string Register = $"{Account}/register";
        public const string EmailConfirmGet = $"{Account}/authenticate_email";
        public const string ConfirmEmailPage = $"{Account}/confirm_email";
        public const string ResendConfirmation = $"{Account}/resend_email";
        public const string EmailConfirmedPage = $"{Account}/email_confirmed";
        public const string SentEmailConfirmation = "/account/sent_confirmation/{Email}";
        public const string Logout = $"{Account}/logout";
        public const string LogoutPage = $"{Account}/logged_out";
        public const string ForgotPassword = $"{Account}/forgot_password";
        public const string ChangeForgotPassword = $"{Account}/reset_password";
        public const string PasswordResetRequested = $"{Account}/password_reset_requested";
        public const string ResetAccountPassword = $"{Account}/reset_account_password";
        public const string AccountLockedOut = $"{Account}/account_locked_out";
    }

    internal static class Debug
    {
        public const string SignIn = "/debug/sign_in";
    }

    internal static class Forms
    {
        public const string Base = "/forms";
        public const string Dash = $"{Base}/dash";
        public const string Create = $"{Base}/create";
        public const string ViewUrl = "/forms/form/{Id:guid}";
        public const string View = "/forms/form";
    }
}
