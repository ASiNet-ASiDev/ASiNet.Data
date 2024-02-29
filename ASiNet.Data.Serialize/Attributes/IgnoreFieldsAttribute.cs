namespace ASiNet.Data.Serialization.Attributes;

/// <summary>
/// Ignore all fields of the object. IT IS USED ONLY WHEN CREATING A MODEL!
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class IgnoreFieldsAttribute : Attribute
{
}
