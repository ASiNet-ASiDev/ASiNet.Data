﻿using System.Text;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators;
public class StringModelsGenerator : IModelsGenerator
{
    public bool CanGenerateModelForType(Type type) =>
        type == typeof(string) ||
        type == typeof(UTF8String) ||
        type == typeof(UTF32String) ||
        type == typeof(UnicodeString) ||
        type == typeof(ASCIIString) ||
        type == typeof(Latin1String);

    public bool CanGenerateModelForType<T>() => CanGenerateModelForType(typeof(T));

    public SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        SerializeModel<T>? result = typeof(T).Name switch
        {
            nameof(String) => Cast(new DefaultStringModel(settings.DefaultEncoding)),
            nameof(UTF8String) => Cast(new StringModel<UTF8String>(Encoding.UTF8)),
            nameof(UTF32String) => Cast(new StringModel<UTF32String>(Encoding.UTF32)),
            nameof(UnicodeString) => Cast(new StringModel<UnicodeString>(Encoding.Unicode)),
            nameof(ASCIIString) => Cast(new StringModel<ASCIIString>(Encoding.ASCII)),
            nameof(Latin1String) => Cast(new StringModel<Latin1String>(Encoding.Latin1)),
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
