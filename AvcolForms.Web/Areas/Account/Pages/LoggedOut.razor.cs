namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// The logged out page of the application
/// </summary>
public partial class LoggedOut
{
    /// <summary>
    /// Seconds before the redirect occurs
    /// </summary>
    public int Seconds { get; set; } = 5;

    protected override void OnAfterRender(bool firstRender)
    {
        for (; Seconds > 0; Seconds--)
        {
            Thread.Sleep(1000);
            StateHasChanged();
        }

        NavManager.NavigateTo("/account/login");
    }
}
