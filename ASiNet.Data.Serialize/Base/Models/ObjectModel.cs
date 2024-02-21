using System.Reflection;
using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization.Base.Models.Interfaces;

namespace ASiNet.Data.Serialization.Base.Models;

public class ObjectModel<T>(SetValuesDelegate<T>? set = null, GetValuesDelegate<T>? get = null) : IObjectModel
{
    public Type ObjType => _objType.Value;

    public bool ContainsGetDelegate => _getDelegate is not null;
    public bool ContainsSetDelegate => _setDelegate is not null;

    private Lazy<Type> _objType = new Lazy<Type>(() => typeof(T));

    private SetValuesDelegate<T>? _setDelegate = set;
    private GetValuesDelegate<T>? _getDelegate = get;

    private PropertyInfo[]? _props;

    public IEnumerable<PropertyInfo> EnumirateProps() => _props ?? [];

    public object?[] GetValues(object obj)
    {
        if (obj is T valueObj)
            return GetValues(valueObj);
        throw new Exception();
    }

    public void SetValues(object obj, IEnumerable<object?> values)
    {
        if (obj is T valueObj)
        {
            SetValues(valueObj, values);
            return;
        }
        throw new Exception();
    }

    public object?[] GetValues(T obj)
    {
        if (_getDelegate is null)
            throw new NullReferenceException();
        return _getDelegate(obj);
    }

    public void SetValues(T obj, IEnumerable<object?> values)
    {
        if (_setDelegate is null)
            throw new NullReferenceException();
        _setDelegate(obj, values);
    }

    public void GenerateSubModels(ObjectModelsContext modelsContext, ObjectModelsGenerator generator)
    {
        if (_props is null)
            return;
        var method = typeof(ObjectModelsGenerator).GetMethod(nameof(ObjectModelsGenerator.GenerateModel));
        foreach (var item in _props)
        {
            if (!modelsContext.ContainsModel(item.PropertyType!))
            {
                var gMethod = method!.MakeGenericMethod(item.PropertyType!);
                var model = (IObjectModel?)gMethod.Invoke(generator, []);
                if (model is null)
                    throw new NullReferenceException();
                modelsContext.AddModel(model);

                model.GenerateSubModels(modelsContext, generator);
            }
        }
    }

    internal void SetSetValueeDelegate(SetValuesDelegate<T>? set) =>
        _setDelegate = set;
    internal void SetGetValueeDelegate(GetValuesDelegate<T>? get) =>
        _getDelegate = get;

    internal void SetProps(params PropertyInfo[] props) =>
        _props = props;

    public void Dispose()
    {
        _setDelegate = null;
        _getDelegate = null;
        GC.SuppressFinalize(this);
    }
}
