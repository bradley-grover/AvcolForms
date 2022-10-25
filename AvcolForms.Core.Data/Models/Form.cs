namespace AvcolForms.Core.Data.Models;

/// <summary>
/// Represents a form saved to the database
/// </summary>
[Table("Forms")]
public class Form : KeyedDbRecord
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
    /// Users that receive the form and need to answer
    /// </summary>
    [Required]
    [JsonPropertyName("recipients")]
    public ICollection<ApplicationUser> Recipients { get; set; }

    /// <summary>
    /// The user that created the form to respond to
    /// </summary>
    [Required]
    [JsonPropertyName("createdBy")]
    public ApplicationUser CreatedBy { get; set; }

    /// <summary>
    /// Represents the content of the form
    /// </summary>
    [Required]
    [JsonPropertyName("content")]
    public ICollection<FormContent> Content { get; set; }

    /// <summary>
    /// The email address for form responses to be sent to
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(Constants.MaxEmailLength, ErrorMessage = Constants.MaxEmailLengthError)]
    [JsonPropertyName("receiver")]
    public string Receiver { get; set; }

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
