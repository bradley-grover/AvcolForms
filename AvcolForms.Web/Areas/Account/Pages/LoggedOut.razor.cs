namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// The logged out page of the application
/// </summary>
public partial class LoggedOut
{
    protected override void OnInitialized()
    {
        NavManager.NavigateTo(Routes.Accounts.Login);
    }
}
