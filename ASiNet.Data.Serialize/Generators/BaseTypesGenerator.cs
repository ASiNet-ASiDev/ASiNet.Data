using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization.Generators;
public class BaseTypesGenerator : IModelsGenerator
{
    public bool CanGenerateModelForType(Type type) => !type.IsEnum && type.IsPrimitive && type.IsValueType;
   
    public bool CanGenerateModelForType<T>() => CanGenerateModelForType(typeof(T));


    public SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        SerializeModel<T>? result = typeof(T).Name switch
        {
            nameof(Int32) => Cast(new Int32Model()),
            nameof(UInt32) => Cast(new UInt32Model()),
            nameof(Int16) => Cast(new Int16Model()),
            nameof(UInt16) => Cast(new UInt16Model()),
            nameof(Int64) => Cast(new Int64Model()),
            nameof(UInt64) => Cast(new UInt64Model()),
            nameof(Byte) => Cast(new ByteModel()),
            nameof(SByte) => Cast(new SByteModel()),
            nameof(Double) => Cast(new DoubleModel()),
            nameof(Single) => Cast(new SingleModel()),
            nameof(Char) => Cast(new CharModel()),
            nameof(Boolean) => Cast(new BooleanModel()),
            _ => null,
        };

        static SerializeModel<T>? Cast(ISerializeModel model) =>
            model as SerializeModel<T>;

        return result is null ? throw new GeneratorException(new NotImplementedException()) : result;
    }


    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }
}
