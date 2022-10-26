namespace AvcolForms.Core.Data.Models;

#nullable disable

/// <summary>
/// Represents a section in a <see cref="Form"/>
/// </summary>
[Table("FormContent")]
public class FormContent : KeyedDbRecord
{
    /// <summary>
    /// The type of item that the form content contains within it
    /// </summary>
    [JsonPropertyName("contentType")]
    [Required]
    public FormContentType ContentType { get; set; }

    /// <summary>
    /// The data used for the form content
    /// </summary>
    [JsonPropertyName("data")]
    [Required]
    public string Data { get; set; }
}
