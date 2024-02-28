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

    private Dictionary<Type, ISerializeModel> _models = [];

    private List<(Predicate<Type> Comparer, IModelsGenerator Generator)> _generators = [
        (type => type.IsEnum, 
            new EnumsModelsGenerator()),

        (type => type.IsValueType && Nullable.GetUnderlyingType(type) is not null, 
            new NullableModelsGenerator()),

        (type => type.IsValueType && !type.IsEnum, 
            new StructsModelsGenirator()),
        
        (type => type.IsArray, 
            new ArraysModelsGenerator()),

        (type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>), 
            new ListModelsGenerator()),

        (type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>), 
            new DictionaryModelsGenerator()),
        ];

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
        
        var generator = _generators.FirstOrDefault(x => x.Comparer.Invoke(type)).Generator 
            ?? ObjectsGenerator;

        var newModel = (generator.GenerateModel<T>(this, BinarySerializer.Settings)) 
            ?? throw new GeneratorException(new NullReferenceException("model is null"));

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
