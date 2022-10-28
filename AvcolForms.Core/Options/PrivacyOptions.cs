/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Options for the privacy policy
/// </summary>
public sealed class PrivacyOptions
{
    /// <summary>
    /// The file name that the privacy policy is stored in
    /// </summary>
    [Required]
    public string FileName { get; set; }
}
