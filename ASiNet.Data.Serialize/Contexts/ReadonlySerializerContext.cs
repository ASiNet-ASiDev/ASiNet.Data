using System.Collections.Frozen;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Contexts;
public class ReadonlySerializerContext : BaseSerializerContext
{
    public ReadonlySerializerContext(GeneratorsSettings settings, params Type[] types) : base(settings)
    {
        var defaultContext = new DefaultSerializerContext(settings);
        foreach (var type in types)
            defaultContext.GetOrGenerate(type);

        _models = defaultContext.GetModels().ToFrozenDictionary();
        _modelsByHash = defaultContext.GetModelsByHash().ToFrozenDictionary();
    }

    private readonly FrozenDictionary<Type, ISerializeModel> _models;

    private FrozenDictionary<long, ISerializeModel> _modelsByHash;

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

    public override void AddGegerator(IModelsGenerator generator) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override bool RemoveGegerator(IModelsGenerator generator) => throw new ContextException(new NotImplementedException("Method not supported!"));

    public override ISerializeModel GetModelByHash(long hash)
    {
        if (_modelsByHash.TryGetValue(hash, out ISerializeModel? model))
            return model;

        throw new ContextException(new ArgumentException("The model for this hash was not found."));
    }

    public override ISerializeModel GetOrGenerateByHash(long hash)
    {
        if (_modelsByHash.TryGetValue(hash, out ISerializeModel? model))
            return model;

        throw new ContextException(new NotImplementedException("This context does not support hash key model generation."));
    }

    public override long RegisterModel<T>() => throw new ContextException(new NotImplementedException("Method not supported!"));
    public override long RegisterModel(Type type) => throw new ContextException(new NotImplementedException("Method not supported!"));
}
