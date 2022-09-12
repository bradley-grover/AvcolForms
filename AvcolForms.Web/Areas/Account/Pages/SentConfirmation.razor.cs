using System.Text;

namespace AvcolForms.Web.Areas.Account.Pages;

/// <summary>
/// Page to be displayed to the user when the email has been sent
/// </summary>
public partial class SentConfirmation
{
    [Parameter]
    public string? Email { get; set; }

#nullable disable
    [Inject]
    private NavigationManager NavManager { get; set; }
#nullable restore

    private readonly List<BreadcrumbItem> items = new()
    {
        new("Home", href: "#", disabled: false, Icons.Material.Filled.Home),
        new("Account", href: null, disabled: true, icon: Icons.Material.Filled.AccountCircle),
        new("Confirm Your Email", href: null, disabled: true, icon: Icons.Material.Filled.ConfirmationNumber)
    };

    protected override void OnParametersSet()
    {
        if (Email is null)
        {
            return;
        }

        var stackMemorySizeNeeded = 4 * Math.Ceiling((double)Constants.MaxEmailLength / 3);

        Span<byte> bytes = stackalloc byte[(int)stackMemorySizeNeeded];

        if (Convert.TryFromBase64String(Email, bytes, out int bytesWritten))
        {
            Email = $"Sent email confirmation to: {Encoding.UTF8.GetString(bytes[..bytesWritten])}";
        }
        else
        {
            Email = "Email is invalid";
        }
    }

    public void GoToResend()
    {
        NavManager.NavigateTo(Routes.Accounts.ResendConfirmation);
    }
}
