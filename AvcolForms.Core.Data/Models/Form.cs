namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a form saved to the database
/// </summary>
public class Form : DbRecord
{
    /// <summary>
    /// The close date of the form if any is set
    /// </summary>
    [JsonPropertyName("closes")]
    public DateTimeOffset? Closes { get; set; }

    // null excluded properties
#nullable disable
    /// <summary>
    /// The title of the user form
    /// </summary>
    [Required]
    [JsonPropertyName("title")]
    [StringLength(Constants.MaxFormTitleLength, ErrorMessage = Constants.MaxFormTitleLengthError)]
    public string Title { get; set; }

    /// <summary>
    /// The description of the user form, short message
    /// </summary>
    [Required]
    [JsonPropertyName("description")]
    [StringLength(Constants.MaxFormDescLength, ErrorMessage = Constants.MaxFormDescLengthError)]
    public string Description { get; set; }

    /// <summary>
    /// Gets the time left before the form expires
    /// </summary>
    [JsonIgnore, NotMapped] // ignore from db & serialization
    public TimeSpan TimeLeft
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (Closes == null)
            {
                return TimeSpan.Zero;
            }

            return Closes >= DateTimeOffset.Now ? TimeSpan.Zero : Closes.Value - DateTimeOffset.Now;
        }
    }
}
