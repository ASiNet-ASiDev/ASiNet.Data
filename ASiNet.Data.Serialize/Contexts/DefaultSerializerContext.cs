using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.Interfaces;

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
    }

    private ObjectsModelsGenerator _objectsGenerator { get; init; } = new();

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

    public Dictionary<Type, ISerializeModel> GetModels() =>
        _models;

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

        var generator = _generators.FirstOrDefault(x => x.Comparer.Invoke(type)).Generator
            ?? _objectsGenerator;

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

        var generator = _generators.FirstOrDefault(x => x.Comparer.Invoke(type)).Generator
            ?? _objectsGenerator;

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

    public override void AddGegerator(Predicate<Type> Comparer, IModelsGenerator Generator) =>
        _generators.Add((Comparer, Generator));

    public override bool RemoveGegerator(IModelsGenerator Generator)
    {
        var it = _generators.FirstOrDefault(x => x.Generator == Generator);
        if (it == default)
            return false;
        return _generators.Remove(it);
    }

    public override bool ContainsModel<T>() =>
        _models.ContainsKey(typeof(T));

    public override bool ContainsModel(Type type) =>
        _models.ContainsKey(type);
}
