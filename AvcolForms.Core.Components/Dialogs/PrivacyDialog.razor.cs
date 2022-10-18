using AvcolForms.Core.Privacy;

namespace AvcolForms.Core.Components.Dialogs;

/// <summary>
/// Privacy dialog for the user to agree to
/// </summary>
public partial class PrivacyDialog
{
#nullable disable
    [CascadingParameter] 
    MudDialogInstance MudDialog { get; set; }

    [Inject] 
    IPrivacyRetriever Privacy { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _loading = true;

        _content = await Privacy.RetrieveAsync();

        _loading = false;
    }

    /// <summary>
    /// Content to display
    /// </summary>
    private string _content;

    /// <summary>
    /// Flag for when the content is getting loaded
    /// </summary>
    private bool _loading = false;

    /// <summary>
    /// Closes the privacy dialog
    /// </summary>
    private void Ok()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }
}
