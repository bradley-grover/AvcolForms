/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Settings for the website
/// </summary>
public sealed class WebsiteOptions
{
    /// <summary>
    /// Title of the website
    /// </summary>
    [Required]
    public string WebsiteTitle { get; set; }
}
