namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents an abstract database record which contains useful metadata on the database record inherited from
/// </summary>
public abstract class DbRecord
{
    /// <summary>
    /// Represents the date that the record in the database was inserted, which contains an offset as well as the date
    /// </summary>
    [Required]
    [Display(Name = "Date Created")]
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Represents the date that the database record was last modified by
    /// </summary>
    [Required]
    [Display(Name = "Date Modified")]
    [JsonPropertyName("modified")]
    public DateTimeOffset Modified { get; set; }
}
