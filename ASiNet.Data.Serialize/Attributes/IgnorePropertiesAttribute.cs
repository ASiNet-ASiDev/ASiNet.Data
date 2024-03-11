namespace ASiNet.Data.Serialization.Attributes;

/// <summary>
/// Ignore all properties of the object. IT IS USED ONLY WHEN CREATING A MODEL!
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
public class IgnorePropertiesAttribute : Attribute
{
}
