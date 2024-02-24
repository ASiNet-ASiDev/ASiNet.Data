using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Models.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization;

/// <summary>
/// Содержит сериализаторы.
/// </summary>
/// <param name="omContext"></param>
public class SerializerContext()
{
    public static SerializerContext FromDefaultModels()
    {
        var context = new SerializerContext();
        context.AddModel(new ByteModel());
        context.AddModel(new SByteModel());

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

        context.AddModel(new GuidModel());
        // ARRAYS
        context.AddModel(new BooleanArrayModel());

        context.AddModel(new Int16ArrayModel());
        context.AddModel(new UInt16ArrayModel());

        context.AddModel(new Int32ArrayModel());
        context.AddModel(new UInt32ArrayModel());

        context.AddModel(new Int64ArrayModel());
        context.AddModel(new UInt64ArrayModel());

        context.AddModel(new SingleArrayModel());
        context.AddModel(new DoubleArrayModel());

        context.AddModel(new ByteArrayModel());
        context.AddModel(new SByteArrayModel());

        context.AddModel(new GuidArrayModel());
        context.AddModel(new DateTimeArrayModel());

        return context;
    }

    public ObjectsSerializerModelsGenerator ObjectsGenerator { get; init; } = new();
    public StructsSerializeModelGenirator StructGenerator { get; init; } = new();

    private Dictionary<Type, ISerializeModel> _models = [];

    public void AddModel(ISerializeModel model)
        => _models.Add(model.ObjType, model);

    public void AddModel<T>(SerializeModel<T> model)
        => _models.Add(typeof(T), model);


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
        else if(Nullable.GetUnderlyingType(type) is not null)
        {
            var nullableModel = new NullableTypesModel<T>();
            _models.TryAdd(type, nullableModel);
            return nullableModel!;
        }
        else if (type.IsValueType)
        {
            var newStructModel = StructGenerator.GenerateModel<T>(this);
            return newStructModel;
        }
        else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var listModel = new ListModel<T>();
            _models.TryAdd(type, listModel);
            return listModel;
        }
        else
        {
            var newObjectModel = ObjectsGenerator.GenerateModel<T>(this);
            return newObjectModel;
        }
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
