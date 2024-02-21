using ASiNet.Data.Base.Serialization.Models;

namespace ASiNet.Data.Serialization.Base.Models.Interfaces;
public interface IObjectModel : IDisposable
{
    public Type ObjType { get; }

    public bool ContainsGetDelegate { get; }
    public bool ContainsSetDelegate { get; }

    public object?[] GetValues(object obj);

    public void SetValues(object obj, IEnumerable<object?> values);

    public void GenerateSubModels(ObjectModelsContext modelsContext, ObjectModelsGenerator generator);
}
