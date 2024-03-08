﻿using System.Linq.Expressions;
using ASiNet.Data.Serialization.Contexts;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Generators.Helpers;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators;
public class ArraysModelsGenerator : IModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
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

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var itemsType = type.GetElementType()!;

        var inst = Expression.Parameter(type, "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(itemsType, serializeContext);
        var intModel = ExpressionsHelper.GetOrGenerateModelGenerateTime(typeof(int), serializeContext);

        var count = Expression.Parameter(typeof(int), "count");

        var body = Expression.IfThenElse(
                // CHECK NULL VALUE
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block([count],
                    ExpressionsHelper.WriteNullableByteGenerateTime(writer, 1),
                    Expression.Assign(count, GetLength(inst)),
                    ExpressionsHelper.CallSerialize(intModel, count, writer),
                    SerializeElements(count, inst, model, writer)
                    ),
                ExpressionsHelper.WriteNullableByteGenerateTime(writer, 0)
                );

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var itemsType = type.GetElementType()!;

        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(itemsType, serializeContext);
        var intModel = ExpressionsHelper.GetOrGenerateModelGenerateTime(typeof(int), serializeContext);

        var inst = Expression.Parameter(type, "inst");
        var count = Expression.Parameter(typeof(int), "count");

        var body = Expression.Block([inst, count],
            Expression.IfThen(
                // READ NULLABLE BYTE
                Expression.Equal(
                    ExpressionsHelper.ReadNullableByteGenerateTime(reader),
                    Expression.Constant((byte)1)),

                Expression.Block(
                    Expression.Assign(count, ExpressionsHelper.CallDeserialize(intModel, reader)),
                    Expression.Assign(inst, Expression.NewArrayBounds(itemsType, count)),
                    DeserializeElements(count, inst, model, reader)
                    )
                ),

            inst);

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(body, reader);
        return lambda.Compile();
    }

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var underlyingType = type.GetElementType()!;

        var inst = Expression.Parameter(typeof(T), "inst");
        var result = Expression.Parameter(typeof(int), "size");
        var count = Expression.Parameter(typeof(int), "count");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(underlyingType, serializeContext);

        var body = Expression.Block([result, count],
            Expression.Assign(result, Expression.Constant(1, typeof(int))),
            Expression.IfThen(
                Expression.NotEqual(
                    inst,
                    Expression.Default(type)),
                Expression.Block(
                    Expression.Assign(count, GetLength(inst)),
                    Expression.AddAssign(result, Expression.Constant(4, typeof(int))),
                    GetElementsSize(count, inst, model, result)
                    )
                ),
            result
            );

        var lambda = Expression.Lambda<GetObjectSizeDelegate<T>>(body, inst);
        return lambda.Compile();
    }

    private Expression GetLength(Expression inst) =>
        Expression.ArrayLength(inst);

    private Expression SerializeElements(Expression count, Expression array, Expression model, Expression writer)
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
                        ExpressionsHelper.CallSerialize(model, Expression.ArrayAccess(array, i), writer),
                        Expression.AddAssign(i, Expression.Constant(1)))
                    ),
                breakLabel)
            );
    }

    private Expression DeserializeElements(Expression count, Expression array, Expression model, Expression reader)
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
                        Expression.Assign(Expression.ArrayAccess(array, i), ExpressionsHelper.CallDeserialize(model, reader)),
                        Expression.AddAssign(i, Expression.Constant(1)))
                    ),
                breakLabel)
            );
    }

    private Expression GetElementsSize(Expression count, Expression array, Expression model, Expression result)
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
                        Expression.AddAssign(result, ExpressionsHelper.CallGetSizeGenerateTime(model, Expression.ArrayAccess(array, i))),
                        Expression.AddAssign(i, Expression.Constant(1)))
                    ),
                breakLabel)
            );
    }
}
