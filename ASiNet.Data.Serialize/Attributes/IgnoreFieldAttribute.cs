namespace ASiNet.Data.Serialization.Attributes;

/// <summary>
/// Ignore the current field. IT IS USED ONLY WHEN CREATING A MODEL!
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class IgnoreFieldAttribute : Attribute
{
}
