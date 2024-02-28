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

        var model = SerializerHelper.GetOrGenerateSerializeModelConstant(itemsType, serializeContext);
        var intModel = SerializerHelper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);

        var count = Expression.Parameter(typeof(int), "count");

        var body = Expression.IfThenElse(
                // CHECK NULL VALUE
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block([count],
                    SerializerHelper.WriteNullableByte(writer, 1),
                    Expression.Assign(count, GetCount(inst)),
                    SerializerHelper.CallSerialize(intModel, count, writer),
                    SerializeElements(count, inst, model, writer)
                    ),
                SerializerHelper.WriteNullableByte(writer, 0)
                );

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }


    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var itemsType = type.GetGenericArguments().First();

        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");

        var model = SerializerHelper.GetOrGenerateSerializeModelConstant(itemsType, serializeContext);
        var intModel = SerializerHelper.GetOrGenerateSerializeModelConstant(typeof(int), serializeContext);

        var inst = Expression.Parameter(type, "inst");
        var count = Expression.Parameter(typeof(int), "count");


        var ctor = type.GetConstructor([typeof(int)])!;

        var body = Expression.Block([inst, count],
            Expression.IfThen(
                // READ NULLABLE BYTE
                Expression.Equal(
                    SerializerHelper.ReadNullableByte(reader),
                    Expression.Constant((byte)1)),

                Expression.Block(
                    Expression.Assign(count, SerializerHelper.CallDeserialize(intModel, reader)),
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
                        SerializerHelper.CallSerialize(model, Expression.Property(list, "Item", i), writer),
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
                        Expression.Call(list, nameof(List<byte>.Add), null, SerializerHelper.CallDeserialize(model, reader)),
                        Expression.AddAssign(i, Expression.Constant(1)))  
                    ),
                breakLabel)
            );
    }
}
