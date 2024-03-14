using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Contexts;
public abstract class BaseSerializerContext(GeneratorsSettings settings) : ISerializerContext
{
    protected GeneratorsSettings Settings { get; init; } = settings;

    public abstract void AddGegerator(IModelsGenerator generator);

    public abstract void AddModel(ISerializeModel model);

    public abstract void AddModel<T>(SerializeModel<T> model);

    public abstract bool ContainsModel(Type type);
    public abstract bool ContainsModel<T>();
    public abstract ISerializeModel GenerateModel(Type type);
    public abstract SerializeModel<T> GenerateModel<T>();

    public abstract ISerializeModel? GetModel(Type type);

    public abstract SerializeModel<T>? GetModel<T>();

    public abstract ISerializeModel GetOrGenerate(Type type);

    public abstract SerializeModel<T> GetOrGenerate<T>();


    public abstract ISerializeModel GetModelByHash(long hash);
    public abstract ISerializeModel GetOrGenerateByHash(long hash);


    public abstract bool RemoveGegerator(IModelsGenerator Generator);

    public abstract bool RemoveModel(ISerializeModel model);

    public abstract bool RemoveModel<T>(SerializeModel<T> model);
}
