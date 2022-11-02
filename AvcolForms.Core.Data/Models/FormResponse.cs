namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a response to a form
/// </summary>
[Table("FormResponse")]
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

    /// <summary>
    /// Responses to certain elements
    /// </summary>
    [JsonPropertyName("responses")]
    public ICollection<FormContentResponse> Responses { get; set; }
}
