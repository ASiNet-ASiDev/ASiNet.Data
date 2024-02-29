namespace ASiNet.Data.Serialization.Attributes;

/// <summary>
/// Ignore the current property . IT IS USED ONLY WHEN CREATING A MODEL!
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnorePropertyAttribute : Attribute
{
}
