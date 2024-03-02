using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators;
public class DictionaryModelsGenerator : IModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        try
        {
            var model = new SerializeModel<T>();

            serializeContext.AddModel(model);

            model.SetSerializeDelegate(GenerateSerializeLambda<T>(serializeContext, settings));
            model.SetDeserializeDelegate(GenerateDeserializeLambda<T>(serializeContext, settings));
            model.SetGetSizeDelegate(GenerateGetSizeDelegate<T>(serializeContext, settings));

            return model;
        }
        catch (Exception ex)
        {
            throw new GeneratorException(ex);
        }
    }

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var keyType = type.GenericTypeArguments[0];
        var valueType = type.GenericTypeArguments[1];

        var inst = Expression.Parameter(typeof(T), "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");

        var keysEnumirator = Expression.Parameter(typeof(IEnumerator<>).MakeGenericType(keyType), "ke");
        var valuesEnumirator = Expression.Parameter(typeof(IEnumerator<>).MakeGenericType(valueType), "ve");
        
        var intModel = Helper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);
        var keyModel = Helper.GetOrGenerateSerializeModelConstant(keyType, serializeContext);
        var valueModel = Helper.GetOrGenerateSerializeModelConstant(valueType, serializeContext);

        var body = 
            Expression.IfThenElse(
                // CHECK NULL VALUE
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block([keysEnumirator, valuesEnumirator],
                    Helper.WriteNullableByte(writer, 1),
                    Helper.CallSerialize(intModel, GetCount(inst), writer),
                    Expression.Assign(keysEnumirator, GetKeysEnumirator(inst, keyType)),
                    Expression.Assign(valuesEnumirator, GetValuesEnumirator(inst, valueType)),
                    SerializeDictionary(keysEnumirator, valuesEnumirator, keyModel, valueModel, writer)
                ),
                Helper.WriteNullableByte(writer, 0)
            );

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }


    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var keyType = type.GenericTypeArguments[0];
        var valueType = type.GenericTypeArguments[1];

        var inst = Expression.Parameter(typeof(T), "inst");
        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");
        var count = Expression.Parameter(typeof(int), "count");

        var intModel = Helper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);
        var keyModel = Helper.GetOrGenerateSerializeModelConstant(keyType, serializeContext);
        var valueModel = Helper.GetOrGenerateSerializeModelConstant(valueType, serializeContext);

        var ctor = type.GetConstructor([typeof(int)])!;

        var body = Expression.Block([inst],
            Expression.IfThen(
                Expression.Equal(
                    Helper.ReadNullableByte(reader),
                    Expression.Constant((byte)1)),
                Expression.Block([count],
                    Expression.Assign(count, Helper.CallDeserialize(intModel, reader)),
                    Expression.Assign(inst, Expression.New(ctor, count)),
                    DeserializeDictionary(count, inst, keyModel, valueModel, reader)
                    )
                ),
            inst
            );

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(body, reader);
        return lambda.Compile();
    }

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var keyType = type.GenericTypeArguments[0];
        var valueType = type.GenericTypeArguments[1];

        var inst = Expression.Parameter(typeof(T), "inst");
        var result = Expression.Parameter(typeof(int), "size");

        var keysEnumirator = Expression.Parameter(typeof(IEnumerator<>).MakeGenericType(keyType), "ke");
        var valuesEnumirator = Expression.Parameter(typeof(IEnumerator<>).MakeGenericType(valueType), "ve");

        var keyModel = Helper.GetOrGenerateSerializeModelConstant(keyType, serializeContext);
        var valueModel = Helper.GetOrGenerateSerializeModelConstant(valueType, serializeContext);

        var body = Expression.Block([result],
            Expression.Assign(result, Expression.Constant(1, typeof(int))),
            Expression.IfThen(
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block([keysEnumirator, valuesEnumirator],
                    Expression.Assign(keysEnumirator, GetKeysEnumirator(inst, keyType)),
                    Expression.Assign(valuesEnumirator, GetValuesEnumirator(inst, valueType)),
                    Expression.AddAssign(result, Expression.Constant(4, typeof(int))),
                    GetElementsSize(keysEnumirator, valuesEnumirator, keyModel, valueModel, result)
                    )
                ),
            result
            );

        var lambda = Expression.Lambda<GetObjectSizeDelegate<T>>(body, inst);
        return lambda.Compile();
    }

    private Expression DeserializeDictionary(Expression count, Expression inst, Expression keyMode, Expression valueModel, Expression reader)
    {
        var i = Expression.Parameter(typeof(int), "i");
        var breakLabel = Expression.Label("LoopBreak");
        return 
            Expression.Block([i],
                Expression.Assign(i, Expression.Constant(0)),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.Equal(i, count),

                        Expression.Break(breakLabel),

                        Expression.Block(
                            Expression.Call(
                                inst, 
                                nameof(Dictionary<byte, byte>.Add), 
                                null, 
                                Helper.CallDeserialize(keyMode, reader), 
                                Helper.CallDeserialize(valueModel, reader)),

                            Expression.AddAssign(i, Expression.Constant(1)))
                        ),
                    breakLabel)
                );
    }

    private Expression SerializeDictionary(Expression keysEnumirator, Expression valuesEnumirator, Expression keyMode, Expression valueModel, Expression writer)
    {
        var breakLabel = Expression.Label("LoopBreak");
        return
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.And(
                        Helper.CallEnumiratorMoveNext(keysEnumirator),
                        Helper.CallEnumiratorMoveNext(valuesEnumirator)
                        ),
                    Expression.Block(
                        Helper.CallSerialize(keyMode, Helper.CallEnumiratorCurrent(keysEnumirator), writer),
                        Helper.CallSerialize(valueModel, Helper.CallEnumiratorCurrent(valuesEnumirator), writer)
                        ),
                    Expression.Break(breakLabel)
                    ),
                breakLabel);
    }

    private Expression GetElementsSize(Expression keysEnumirator, Expression valuesEnumirator, Expression keyMode, Expression valueModel, Expression result)
    {
        var breakLabel = Expression.Label("LoopBreak");
        return 
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.And(
                        Helper.CallEnumiratorMoveNext(keysEnumirator),
                        Helper.CallEnumiratorMoveNext(valuesEnumirator)
                        ),
                    Expression.Block(
                        Expression.AddAssign(result, Helper.CallGetSize(keyMode, Helper.CallEnumiratorCurrent(keysEnumirator))),
                        Expression.AddAssign(result, Helper.CallGetSize(valueModel, Helper.CallEnumiratorCurrent(valuesEnumirator)))
                        ),
                    Expression.Break(breakLabel)
                ),
            breakLabel);
    }

    private Expression GetCount(Expression inst) =>
        Expression.Property(inst, nameof(ICollection<byte>.Count));


    private Expression GetKeysEnumirator(Expression dictionary, Type valueType) =>
        Helper.CallGetEnumirator(Expression.Property(dictionary, nameof(Dictionary<byte, byte>.Keys)), valueType);

    private Expression GetValuesEnumirator(Expression dictionary, Type enumType) =>
       Helper.CallGetEnumirator(Expression.Property(dictionary, nameof(Dictionary<byte, byte>.Values)), enumType);
}
