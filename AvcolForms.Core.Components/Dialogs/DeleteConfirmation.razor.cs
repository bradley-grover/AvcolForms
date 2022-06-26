namespace AvcolForms.Core.Components.Dialogs;

/// <summary>
/// Delete confirmation, second after a user presses a dangerous action
/// </summary>
public partial class DeleteConfirmation
{
#nullable disable
    /// <summary>
    /// Mud Dialog instance passed by DI
    /// </summary>
    [CascadingParameter] 
    MudDialogInstance MudDialog { get; set; }

    /// <summary>
    /// Content to be displayed
    /// </summary>
    [Parameter] 
    public string ContentText { get; set; }

    /// <summary>
    /// Button text passed in by the library user
    /// </summary>
    [Parameter] 
    public string ButtonText { get; set; }

    /// <summary>
    /// Color of the button
    /// </summary>
    [Parameter]
    public Color Color { get; set; }
#nullable restore

    /// <summary>
    /// Submits the dialog
    /// </summary>
    private void Submit() => MudDialog.Close(DialogResult.Ok(true));

    /// <summary>
    /// Cancels the dialog
    /// </summary>
    private void Cancel() => MudDialog.Cancel();
}
