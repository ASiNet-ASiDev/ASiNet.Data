using ASiNet.Data.Serialization.Exceptions;
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

    public ObjectsModelsGenerator ObjectsGenerator { get; init; } = new();
    public StructsModelGenirator StructGenerator { get; init; } = new();
    public EnumsModelGenerator EnumGenerator { get; init; } = new();
    public NullableTypesGenerator NullableGenerator { get; init; } = new();


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
        SerializeModel<T>? newModel;
        if (type.IsArray)
            newModel = new ArrayModel<T>();
        else if (type.IsEnum)
            newModel = EnumGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        else if (Nullable.GetUnderlyingType(type) is not null)
            newModel = NullableGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        else if (type.IsValueType)
            newModel = StructGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        else if (type.IsGenericType)
        {
            var def = type.GetGenericTypeDefinition();
            if (def == typeof(List<>))
                newModel = new ListModel<T>();
            else if (def == typeof(Dictionary<,>))
                newModel = (SerializeModel<T>)Activator.CreateInstance(typeof(DictionaryModel<>).MakeGenericType(type))!;
            else
                newModel = ObjectsGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        }
        else
            newModel = ObjectsGenerator.GenerateModel<T>(this, BinarySerializer.Settings);
        if(newModel is null)
            throw new GeneratorException(new NullReferenceException("model is null"));
        _models.TryAdd(type, newModel);
        return newModel;
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
