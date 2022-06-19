using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Settings for the website
/// </summary>
public class WebsiteOptions
{
    /// <summary>
    /// Title of the website
    /// </summary>
    [Required]
    public string WebsiteTitle { get; set; }
}
