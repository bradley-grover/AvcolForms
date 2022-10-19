namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a response to a form
/// </summary>
public class FormResponse : DbRecord
{
#nullable disable
    /// <summary>
    /// User that responded to the form
    /// </summary>
    [JsonPropertyName("user")]
    public ApplicationUser User { get; set; }
}
