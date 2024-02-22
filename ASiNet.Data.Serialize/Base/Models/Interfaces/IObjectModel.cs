using ASiNet.Data.Base.Serialization.Models;

namespace ASiNet.Data.Serialization.Base.Models.Interfaces;
public interface IObjectModel : IDisposable
{
    public Type ObjType { get; }

    public int PropertiesCount { get; }

    public bool ContainsGetDelegate { get; }
    public bool ContainsSetDelegate { get; }

    public object?[] GetValues(object obj);

    public void SetValues(object obj, object?[] values);
}
