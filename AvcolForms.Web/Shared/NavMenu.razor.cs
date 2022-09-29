using AvcolForms.Core.Components.Dialogs;
using AvcolForms.Core.Options;
using Microsoft.Extensions.Options;

namespace AvcolForms.Web.Shared;

public partial class NavMenu
{
    private static bool IsDebug =>
#if DEBUG
    true;
#else
    false;
#endif

    async Task Logout()
    {
        var parameters = new DialogParameters
        {
            { "ContentText", "Do you really want to logout?" },
            { "ButtonText", "Sign Out" },
            { "Color", Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = Dialog.Show<DeleteConfirmation>("Logout", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            NavManager.NavigateTo(Routes.Accounts.Logout, forceLoad: true);
        }
    }
}
