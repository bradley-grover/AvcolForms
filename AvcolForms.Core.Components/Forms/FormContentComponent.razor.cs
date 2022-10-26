namespace AvcolForms.Core.Components.Forms;

#nullable disable

/// <summary>
/// Provides user interface rendering of a <see cref="FormContent"/>
/// </summary>
public partial class FormContentComponent
{
    /// <summary>
    /// Represents the content to supply the component
    /// </summary>
    [Parameter]
    public FormContent Content { get; set; }
}
