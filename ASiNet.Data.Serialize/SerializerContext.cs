using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization;

/// <summary>
/// Содержит сериализаторы.
/// </summary>
/// <param name="omContext"></param>
public class SerializerContext()
{

    public ObjectsSerializerModelsGenerator ObjectsGenerator { get; init; } = new();
    public StructsSerializeModelGenirator StructGenerator { get; init; } = new();

    private Dictionary<Type, ISerializeModel> _models = [];

    public void AddModel(ISerializeModel model)
        => _models.Add(model.ObjType, model);

    public void AddModel<T>(SerializeModel<T> model)
        => _models.Add(typeof(T), model);

    public void AddOrReplaceModel<T>(SerializeModel<T> model) =>
        AddOrReplaceModel((ISerializeModel)model);

    public void AddOrReplaceModel(ISerializeModel model)
    {
        if (_models.ContainsKey(model.ObjType))
        {
            _models.Remove(model.ObjType);
            _models.Add(model.ObjType, model);
        }
    }

    public bool RemoveModel(ISerializeModel model) =>
        _models.Remove(model.ObjType);

    public bool RemoveModel<T>(SerializeModel<T> model) =>
        _models.Remove(model.ObjType);

    public SerializeModel<T> GetOrGenerate<T>()
    {
        if (GetModel<T>() is SerializeModel<T> model)
            return model;

        var type = typeof(T);
        if (type.IsArray)
        {
            var arrModel = new ArrayModel<T>();
            _models.TryAdd(type, arrModel);
            return arrModel;
        }
        else if (type.IsEnum)
        {
            var enumModel = new EnumModel<T>();
            _models.TryAdd(type, enumModel);
            return enumModel;
        }
        else if (Nullable.GetUnderlyingType(type) is not null)
        {
            var nullableModel = new NullableTypesModel<T>();
            _models.TryAdd(type, nullableModel);
            return nullableModel!;
        }
        else if (type.IsValueType)
        {
            var newStructModel = StructGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
            return newStructModel;
        }
        else if (type.IsGenericType)
        {
            var def = type.GetGenericTypeDefinition();
            if (def == typeof(List<>))
            {
                var listModel = new ListModel<T>();
                _models.TryAdd(type, listModel);
                return listModel;
            }
            else if (def == typeof(Dictionary<,>))
            {
                var dickModel = (SerializeModel<T>)Activator.CreateInstance(typeof(DictionaryModel<>).MakeGenericType(type))!;
                _models.TryAdd(type, dickModel);
                return dickModel;
            }
        }
        var newObjectModel = ObjectsGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        return newObjectModel;
    }

    public ISerializeModel GetOrGenerate(Type type) =>
        (ISerializeModel?)SerializerHelper.InvokeGenerickMethod(this, nameof(this.GetOrGenerate), [type], []) ??
        throw new NullReferenceException();



    public ISerializeModel? GetModel(Type type)
    {
        if (_models.TryGetValue(type, out ISerializeModel? model))
            return model;
        return null;
    }

    public SerializeModel<T>? GetModel<T>()
    {
        if (_models.TryGetValue(typeof(T), out ISerializeModel? model))
            return (SerializeModel<T>)model;
        return null;
    }

    public bool ContainsModel<T>() =>
        _models.ContainsKey(typeof(T));

    public bool ContainsModel(Type type) =>
        _models.ContainsKey(type);
}
