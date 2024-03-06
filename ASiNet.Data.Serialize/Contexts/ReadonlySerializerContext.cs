using System.Collections.Frozen;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Contexts;
public class ReadonlySerializerContext : BaseSerializerContext
{
    public ReadonlySerializerContext(GeneratorsSettings settings) : base(settings)
    {
        var defaultContext = new DefaultSerializerContext(settings);

        _models = defaultContext.GetModels().ToFrozenDictionary();
    }

    private readonly FrozenDictionary<Type, ISerializeModel> _models;


    public override bool ContainsModel<T>() =>
        _models.ContainsKey(typeof(T));

    public override bool ContainsModel(Type type) =>
        _models.ContainsKey(type);

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

    public override ISerializeModel GetOrGenerate(Type type) => GetModel(type) ?? throw new ContextException(new NotImplementedException("Method not supported!"));

    public override SerializeModel<T> GetOrGenerate<T>()
    {
        if (_models.TryGetValue(typeof(T), out var m) && m is SerializeModel<T> model)
            return model;

        throw new ContextException(new NotImplementedException("Method not supported!"));
    }


    public override void AddModel(ISerializeModel model) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override void AddModel<T>(SerializeModel<T> model) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override bool RemoveModel(ISerializeModel model) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override bool RemoveModel<T>(SerializeModel<T> model) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override ISerializeModel GenerateModel(Type type) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override SerializeModel<T> GenerateModel<T>() => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override void AddGegerator(Predicate<Type> Comparer, IModelsGenerator Generator) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override bool RemoveGegerator(IModelsGenerator Generator) => throw new ContextException(new NotImplementedException("Method not supported!"));
}
