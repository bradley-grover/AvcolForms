namespace AvcolForms.Web.Areas.Account;

internal static class AccountRoutes
{
    public const string Account = "/account";

    public const string AuthenticateLogin = $"{Account}/authenticate_login";
    public const string Login = $"{Account}/login";
    public const string Register = $"{Account}/register";
    public const string EmailConfirmGet = $"{Account}/authenticate_email";
    public const string ConfirmEmailPage = $"{Account}/confirm_email";
    public const string ResendConfirmation = $"{Account}/resend_email";
}
