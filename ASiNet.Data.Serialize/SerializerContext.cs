﻿using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;
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
        context.AddModel(new SbyteModel());

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

        return context;
    }

    public SerializerModelsGenerator Generator { get; init; } = new();

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
        else
        {
            var newModel = Generator.GenerateModel<T>(this);
            return newModel;
        }
    }

    public ISerializeModel GetOrGenerate(Type type)
    {
        if (GetModel(type) is ISerializeModel model)
            return model;

        if (type.IsArray)
        {
            var arrModel = (ISerializeModel?)Activator.CreateInstance(typeof(ArrayModel<>).MakeGenericType(type))
                ?? throw new Exception();
            _models.TryAdd(type, arrModel);
            return arrModel;
        }
        else
        {
            var mi = typeof(SerializerModelsGenerator).GetMethod(nameof(SerializerModelsGenerator.GenerateModel))?.MakeGenericMethod(type)
                ?? throw new Exception();
            var newModel = (ISerializeModel?)mi.Invoke(Generator, [this])
                ?? throw new Exception();
            _models.TryAdd(type, newModel);
            return newModel;
        }
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
