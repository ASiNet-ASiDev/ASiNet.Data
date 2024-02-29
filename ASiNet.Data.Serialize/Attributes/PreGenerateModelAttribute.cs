﻿namespace ASiNet.Data.Serialization.Attributes;

/// <summary>
/// Objects marked with this attribute will be created together with the context, if this feature is enabled in the settings.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class PreGenerateModelAttribute() : Attribute
{
}
