namespace AvcolForms.Core.Data.Models;

/// <summary>
/// A keyed database record branced from <see cref="DbRecord"/>
/// </summary>
public abstract class KeyedDbRecord : DbRecord
{
    /// <summary>
    /// The identifier of the database record
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
