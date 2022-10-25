namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a response to a form
/// </summary>
public class FormResponse : KeyedDbRecord
{
#nullable disable
    /// <summary>
    /// User that responded to the form
    /// </summary>
    [JsonPropertyName("user")]
    public ApplicationUser User { get; set; }

    /// <summary>
    /// The form that the user responded to
    /// </summary>
    [JsonPropertyName("form")]
    public Form Form { get; set; }
}
