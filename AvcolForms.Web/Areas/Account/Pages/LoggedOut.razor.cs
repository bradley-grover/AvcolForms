namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// The logged out page of the application
/// </summary>
[Route(Routes.Accounts.LogoutPage)]
public partial class LoggedOut
{
    protected override void OnAfterRender(bool firstRender)
    {
        NavManager.NavigateTo(Routes.Accounts.Login);
    }
}
