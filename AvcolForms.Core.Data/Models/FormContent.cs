namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a section in a <see cref="Form"/>
/// </summary>
public class FormContent : KeyedDbRecord
{
    /// <summary>
    /// The type of item that the form content contains within it
    /// </summary>
    [JsonPropertyName("contentType")]
    [Required]
    public FormContentType ContentType { get; set; }
}
