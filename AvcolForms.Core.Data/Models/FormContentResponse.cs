namespace AvcolForms.Core.Data.Models;

#nullable disable

/// <summary>
/// Response to a form content response
/// </summary>
public class FormContentResponse : KeyedDbRecord
{
    /// <summary>
    /// Data contained in the response
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Responds to this form content
    /// </summary>
    public FormContent RespondsTo { get; set; }
}
