namespace AvcolForms.Core.Components;

/// <summary>
/// Represents state of a password text field
/// </summary>
public sealed class SensitiveStore
{
    public bool Show { get; set; } = false;
    public InputType InputType { get; set; } = InputType.Password;
    public string Icon { get; set; } = Icons.Filled.VisibilityOff;

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
