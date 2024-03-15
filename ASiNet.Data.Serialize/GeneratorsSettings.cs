using System.Text;
using ASiNet.Data.Serialization.Contexts;

namespace ASiNet.Data.Serialization;

/// <summary>
/// Settings for model generators and context. 
/// <para/> 
/// Please note that the settings should be set before creating the context, otherwise they may not be applied!
/// <para/>
/// When changing the settings, it is recommended to recreate the context using <see cref="BinarySerializer.RegenerateContext"/>
/// </summary>
public class GeneratorsSettings
{
    public GeneratorsSettings()
    {
    }

    /// <summary>
    /// Ignore the properties of any object even if they are not marked <see cref="Attributes.IgnorePropertyAttribute"/> or <see cref="Attributes.IgnorePropertiesAttribute"/>
    /// </summary>
    public bool GlobalIgnoreProperties { get; set; }

    /// <summary>
    /// Ignore the fields of any object even if they are not marked <see cref="Attributes.IgnoreFieldAttribute"/> or <see cref="Attributes.IgnoreFieldsAttribute"/>
    /// </summary>
    public bool GlobalIgnoreFields { get; set; }

    /// <summary>
    /// Allow generation of models marked with the <see cref="Attributes.PreGenerateAttribute"/> attribute WHEN CREATING a <see cref="DefaultSerializerContext"/>
    /// </summary>
    public bool AllowPreGenerateModelAttribute { get; set; } = true;


    public bool UseUnsafeArraysModelsGenerator { get; set; } = true;

    public bool UseAdditionalModelsGenerators { get; set; } = true;

    public Encoding DefaultEncoding { get; set; } = Encoding.UTF8;
}
