using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.Base.Models.Interfaces;

namespace ASiNet.Data.Base.Serialization.Models;
/// <summary>
/// Содержит модели обьектов для сериализатора.
/// </summary>
public class ObjectModelsContext
{
    public ObjectModelsGenerator Generator { get; init; } = new();

    private Dictionary<Type, IObjectModel> _models = [];

    public void AddModel(IObjectModel model)
        => _models.Add(model.ObjType, model);

    public void AddModel<T>(ObjectModel<T> model)
        => _models.Add(typeof(T), model);


    public ObjectModel<T> GetOrGenerate<T>()
    {
        if(GetModel<T>() is ObjectModel<T> model)
            return model;
        var newModel = Generator.GenerateModel<T>();
        _models.TryAdd(typeof(T), newModel);
        //newModel.GenerateSubModels(this, Generator);
        return newModel;
    }

    public IObjectModel? GetModel(Type type)
    {
        if (_models.TryGetValue(type, out IObjectModel? model))
            return model;
        return null;
    }

    public ObjectModel<T>? GetModel<T>()
    {
        if (_models.TryGetValue(typeof(T), out IObjectModel? model))
            return (ObjectModel<T>)model;
        return null;
    }

    public bool ContainsModel<T>() =>
        _models.ContainsKey(typeof(T));

    public bool ContainsModel(Type type) =>
        _models.ContainsKey(type);
}
