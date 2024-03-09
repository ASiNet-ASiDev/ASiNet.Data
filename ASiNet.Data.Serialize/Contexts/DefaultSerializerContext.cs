using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Contexts;

/// <summary>
/// Содержит сериализаторы.
/// </summary>
/// <param name="omContext"></param>
public class DefaultSerializerContext : BaseSerializerContext
{
    public DefaultSerializerContext(GeneratorsSettings settings) : base(settings)
    {
        if (Settings.UseDefaultBaseTypesModels)
            Helper.AddUnmanagedTypes(this);
        if (Settings.UseDefaultUnsafeArraysModels)
            Helper.AddUnsafeArraysTypes(this);


        if (Settings.AllowPreGenerateModelAttribute)
        {
            foreach (var type in Helper.EnumiratePreGenerateModels())
            {
                if (!ContainsModel(type))
                    GenerateModel(type);
            }
        }
        _modelsByHash = new(GenerateModelsByHashDictionary);
    }

    private Dictionary<Type, ISerializeModel> _models = [];

    private Lazy<Dictionary<string, ISerializeModel>> _modelsByHash;

    private List<IModelsGenerator> _generators = [
        new EnumsModelsGenerator(),
        new AbstractModelsGenerator(),
        new NullableModelsGenerator(),
        new StructsModelsGenirator(),
        new ArraysModelsGenerator(),
        new ListModelsGenerator(),
        new DictionaryModelsGenerator(),
        new ObjectsModelsGenerator(),
        ];

    public Dictionary<Type, ISerializeModel> GetModels() =>
        _models;

    public Dictionary<string, ISerializeModel> GetModelsByHash() =>
        _modelsByHash.Value;

    public override void AddModel(ISerializeModel model)
        => _models.Add(model.ObjType, model);

    public override void AddModel<T>(SerializeModel<T> model)
        => _models.Add(typeof(T), model);

    public override bool RemoveModel(ISerializeModel model) =>
        _models.Remove(model.ObjType);

    public override bool RemoveModel<T>(SerializeModel<T> model) =>
        _models.Remove(model.ObjType);

    public override ISerializeModel GenerateModel(Type type) =>
        (ISerializeModel?)Helper.InvokeGenerickMethod(this, nameof(this.GenerateModel), [type], []) ??
        throw new NullReferenceException();

    public override SerializeModel<T> GenerateModel<T>()
    {
        var type = typeof(T);

        var generator = _generators.FirstOrDefault(x => x.CanGenerateModelForType(type))
            ?? throw new GeneratorException(new NullReferenceException("Generator not found"));

        var newModel = generator.GenerateModel<T>(this, Settings)
            ?? throw new GeneratorException(new NullReferenceException("model is null"));

        _models.TryAdd(type, newModel);
        return newModel;
    }

    public override ISerializeModel GetOrGenerate(Type type) =>
        (ISerializeModel?)Helper.InvokeGenerickMethod(this, nameof(this.GetOrGenerate), [type], []) ??
        throw new NullReferenceException();

    public override SerializeModel<T> GetOrGenerate<T>()
    {
        if (GetModel<T>() is SerializeModel<T> model)
            return model;

        var type = typeof(T);

        var generator = _generators.FirstOrDefault(x => x.CanGenerateModelForType(type))
            ?? throw new GeneratorException(new NullReferenceException("Generator not found"));

        var newModel = generator.GenerateModel<T>(this, Settings)
            ?? throw new GeneratorException(new NullReferenceException("model is null"));

        _models.TryAdd(type, newModel);
        return newModel;
    }

    public override ISerializeModel? GetModel(Type type)
    {
        if (_models.TryGetValue(type, out ISerializeModel? model))
            return model;
        return null;
    }

    public override SerializeModel<T>? GetModel<T>()
    {
        if (_models.TryGetValue(typeof(T), out ISerializeModel? model))
            return (SerializeModel<T>)model;
        return null;
    }

    public override ISerializeModel GetModelByHash(string hash)
    {
        if (_modelsByHash.Value.TryGetValue(hash, out ISerializeModel? model))
            return model;

        throw new ContextException(new ArgumentException("The model for this hash was not found."));
    }

    public override ISerializeModel GetOrGenerateByHash(string hash)
    {
        if (_modelsByHash.Value.TryGetValue(hash, out ISerializeModel? model))
            return model;

        throw new ContextException(new NotImplementedException("This context does not support hash key model generation."));
    }

    public override void AddGegerator(IModelsGenerator generator) =>
        _generators.Add(generator);

    public override bool RemoveGegerator(IModelsGenerator generator)
    {
        var it = _generators.FirstOrDefault(x => x == generator);
        if (it == default)
            return false;
        return _generators.Remove(it);
    }

    public override bool ContainsModel<T>() =>
        _models.ContainsKey(typeof(T));

    public override bool ContainsModel(Type type) =>
        _models.ContainsKey(type);


    private Dictionary<string, ISerializeModel> GenerateModelsByHashDictionary()
    {
        var result = new Dictionary<string, ISerializeModel>();
        foreach (var item in _models.Values)
        {
            if (!result.TryAdd(item.TypeHash, item))
                throw new ContextException(
                    new Exception($"It was not possible to add a model with <{item.ObjType.FullName}, {item.TypeHash}> cache, since a model with this hash already exists."));
        }

        return result;
    }

    private void AddByHashModel(ISerializeModel model)
    {
        if (_modelsByHash.IsValueCreated)
        {
            if (!_modelsByHash.Value.TryAdd(model.TypeHash, model))
                throw new ContextException(
                    new Exception($"It was not possible to add a model with <{model.ObjType.FullName}, {model.TypeHash}> cache, since a model with this hash already exists."));
        }
    }
}
