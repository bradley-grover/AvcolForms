using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvcolForms.Core.Data.Models.Configuration;

/// <summary>
/// Configuration of the model <see cref="Form"/>
/// </summary>
public class FormModelConfiguration : IEntityTypeConfiguration<Form>
{
    /// <summary>
    /// Configures the model <see cref="Form"/>
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Form> builder) // the most hacky thing i've done
    {
        builder.Property(f => f.Recipients)
            .HasConversion(v => JsonSerializer.Serialize(v, ListSerializationContext.Default.ListString),
            v => JsonSerializer.Deserialize(v, ListSerializationContext.Default.ListString)!);
    }
}
