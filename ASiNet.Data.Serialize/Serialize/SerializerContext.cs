using ASiNet.Data.Base.Models;
using ASiNet.Data.Serialize.Interfaces;
using ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialize;
public class SerializerContext(ObjectModelsContext omContext)
{
    public static SerializerContext FromDefaultModels(ObjectModelsContext omContext)
    {
        var context = new SerializerContext(omContext);
        context.AddModel(new Int16Model());
        context.AddModel(new Int32Model());
        context.AddModel(new Int64Model());
        context.AddModel(new UInt16Model());
        context.AddModel(new UInt32Model());
        context.AddModel(new UInt64Model());
        context.AddModel(new SingleModel());
        context.AddModel(new DoubleModel());
        context.AddModel(new CharModel());
        context.AddModel(new StringModel());
        context.AddModel(new BooleanModel());

        return context;
    }

    public SerializerModelsGenerator Generator { get; init; } = new();
    public ObjectModelsContext ObjectModelsContext { get; init; } = omContext;

    private Dictionary<Type, ISerializeModel> _models = [];

    public void AddModel(ISerializeModel model)
        => _models.Add(model.ObjType, model);

    public void AddModel<T>(SerializeModel<T> model)
        => _models.Add(typeof(T), model);


    public SerializeModel<T> GetOrGenerate<T>()
    {
        if (GetModel<T>() is SerializeModel<T> model)
            return model;
        var newModel = Generator.GenerateModel<T>(ObjectModelsContext, this);
        _models.TryAdd(typeof(T), newModel);
        return newModel;
    }


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
