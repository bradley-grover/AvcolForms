namespace AvcolForms.Core.Data;

/// <summary>
/// Serializes a list of string
/// </summary>
[JsonSerializable(typeof(List<string>))]
public partial class ListSerializationContext : JsonSerializerContext
{
}
