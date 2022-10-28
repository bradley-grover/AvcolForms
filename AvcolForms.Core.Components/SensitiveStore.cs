namespace AvcolForms.Core.Components;

/// <summary>
/// Represents state of a password text field
/// </summary>
public sealed class SensitiveStore
{
    /// <summary>
    /// Whether the field is showing right now
    /// </summary>
    public bool Show { get; internal set; } = false;

    /// <summary>
    /// The input type of the data field
    /// </summary>
    public InputType InputType { get; internal set; } = InputType.Password;

    /// <summary>
    /// The icon of the field
    /// </summary>
    public string Icon { get; internal set; } = Icons.Filled.VisibilityOff;

    public void ToggleState()
    {
        if (Show)
        {
            Show = false;
            InputType = InputType.Password;
            Icon = Icons.Filled.VisibilityOff;
        }
        else
        {
            Show = true;
            Icon = Icons.Filled.Visibility;
            InputType = InputType.Text;
        }
    }
}
