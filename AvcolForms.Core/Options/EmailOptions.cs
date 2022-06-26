/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Email settings used for the application
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// The email address that sends the email
    /// </summary>
    [EmailAddress]
    [Required]
    public string FromEmail { get; set; }

    /// <summary>
    /// The network port that the server connects to
    /// </summary>
    [Range(0, ushort.MaxValue)]
    [Required]
    public int Port { get; set; }

    /// <summary>
    /// The name of the sender
    /// </summary>
    [Required]
    public string SenderName { get; set; }

    /// <summary>
    /// The smtp server being used for the connection
    /// </summary>
    [Required]
    public string SmtpServer { get; set; }

    /// <summary>
    /// The smtp user name
    /// </summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// The password for smtp authentication
    /// </summary>
    [Required]
    public string Password { get; set; }
}
