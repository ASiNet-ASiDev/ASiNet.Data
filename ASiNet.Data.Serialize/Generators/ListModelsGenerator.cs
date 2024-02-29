using System.Linq.Expressions;
using System.Reflection;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Generators;
public class ListModelsGenerator : IModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        try
        {
            var model = new SerializeModel<T>();

            serializeContext.AddModel(model);

            model.SetSerializeDelegate(GenerateSerializeLambda<T>(serializeContext, settings));
            model.SetDeserializeDelegate(GenerateDeserializeLambda<T>(serializeContext, settings));
            model.SetGetSizeDelegate(GenerateGetSerializedObjectSizeDelegate<T>(serializeContext, settings));

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
        var itemsType = type.GetGenericArguments().First();

        var inst = Expression.Parameter(type, "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");

        var model = Helper.GetOrGenerateSerializeModelConstant(itemsType, serializeContext);
        var intModel = Helper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);

        var count = Expression.Parameter(typeof(int), "count");

        var body = Expression.IfThenElse(
                // CHECK NULL VALUE
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block([count],
                    Helper.WriteNullableByte(writer, 1),
                    Expression.Assign(count, GetCount(inst)),
                    Helper.CallSerialize(intModel, count, writer),
                    SerializeElements(count, inst, model, writer)
                    ),
                Helper.WriteNullableByte(writer, 0)
                );

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }


    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var itemsType = type.GetGenericArguments().First();

        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");

        var model = Helper.GetOrGenerateSerializeModelConstant(itemsType, serializeContext);
        var intModel = Helper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);

        var inst = Expression.Parameter(type, "inst");
        var count = Expression.Parameter(typeof(int), "count");


        var ctor = type.GetConstructor([typeof(int)])!;

        var body = Expression.Block([inst, count],
            Expression.IfThen(
                // READ NULLABLE BYTE
                Expression.Equal(
                    Helper.ReadNullableByte(reader),
                    Expression.Constant((byte)1)),

                Expression.Block(
                    Expression.Assign(count, Helper.CallDeserialize(intModel, reader)),
                    Expression.Assign(inst, Expression.New(ctor, count)),
                    DeserializeElements(count, inst, model, reader)
                    )
                ),

            inst);

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(body, reader);
        return lambda.Compile();
    }

    public Expression GetCount(Expression inst) =>
        Expression.Property(inst, nameof(ICollection<byte>.Count));

    public Expression SerializeElements(Expression count, Expression list, Expression model, Expression writer)
    {
        var i = Expression.Parameter(typeof(int), "i");
        var breakLabel = Expression.Label("LoopBreak");
        return Expression.Block([i],
            Expression.Assign(i, Expression.Constant(0)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.Equal(i, count),
                    //
                    Expression.Break(breakLabel),
                    // ADD AND DESERIALIZE ELEMENT
                    Expression.Block(
                        Helper.CallSerialize(model, Expression.Property(list, "Item", i), writer),
                        Expression.AddAssign(i, Expression.Constant(1)))
                    ),
                breakLabel)
            );
    }

    public Expression DeserializeElements(Expression count, Expression list, Expression model, Expression reader)
    {
        var i = Expression.Parameter(typeof(int), "i");
        var breakLabel = Expression.Label("LoopBreak");
        return Expression.Block([i],
            Expression.Assign(i, Expression.Constant(0)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.Equal(i, count),
                    //
                    Expression.Break(breakLabel),
                    // ADD AND DESERIALIZE ELEMENT
                    Expression.Block(
                        Expression.Call(list, nameof(List<byte>.Add), null, Helper.CallDeserialize(model, reader)),
                        Expression.AddAssign(i, Expression.Constant(1)))  
                    ),
                breakLabel)
            );
    }

    public Expression GetElementsSize(Expression count, Expression list, Expression model, Expression result)
    {
        var i = Expression.Parameter(typeof(int), "i");
        var breakLabel = Expression.Label("LoopBreak");
        return Expression.Block([i],
            Expression.Assign(i, Expression.Constant(0)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.Equal(i, count),
                    //
                    Expression.Break(breakLabel),
                    // ADD AND DESERIALIZE ELEMENT
                    Expression.Block(
                        Expression.AddAssign(result, Helper.CallGetSize(model, Expression.Property(list, "Item", i))),
                        Expression.AddAssign(i, Expression.Constant(1)))
                    ),
                breakLabel)
            );
    }

    public GetObjectSizeDelegate<T> GenerateGetSerializedObjectSizeDelegate<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var underlyingType = type.GetGenericArguments().First();

        var inst = Expression.Parameter(typeof(T), "inst");
        var result = Expression.Parameter(typeof(int), "size");
        var count = Expression.Parameter(typeof(int), "count");

        var model = Helper.GetOrGenerateSerializeModelConstant(underlyingType, serializeContext);

        var body = Expression.Block([result, count],
            Expression.Assign(result, Expression.Constant(1, typeof(int))),
            Expression.IfThen(
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block(
                    Expression.Assign(count, GetCount(inst)),
                    Expression.AddAssign(result, Expression.Constant(4, typeof(int))),
                    GetElementsSize(count, inst, model, result)
                    )
                ),
            result
            );

        var lambda = Expression.Lambda<GetObjectSizeDelegate<T>>(body, inst);
        return lambda.Compile();
    }
}
