using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class EnumModel<TEnum> : BaseSerializeModel<TEnum>
    where TEnum : Enum
{
    public override TEnum Deserialize(ISerializeReader reader)
    {
        var etype = typeof(TEnum).GetEnumUnderlyingType();
        
        if (etype == typeof(int))
            return SerializerHelper.ToEnum<int, TEnum>(
                BinarySerializer.SharedSerializeContext
                .GetOrGenerate<int>()
                .Deserialize(reader));

        if (etype == typeof(byte))
            return SerializerHelper.ToEnum<byte, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<byte>()
                    .Deserialize(reader));

        if (etype == typeof(short))
            return SerializerHelper.ToEnum<short, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<short>()
                    .Deserialize(reader));

        if (etype == typeof(long))
            return SerializerHelper.ToEnum<long, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<long>()
                    .Deserialize(reader));

        if (etype == typeof(ushort))
            return SerializerHelper.ToEnum<ushort, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<ushort>()
                    .Deserialize(reader));

        if (etype == typeof(uint))
            return SerializerHelper.ToEnum<uint, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<uint>()
                    .Deserialize(reader));

        if (etype == typeof(ulong))
            return SerializerHelper.ToEnum<ulong, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<ulong>()
                    .Deserialize(reader));

        if (etype == typeof(sbyte))
            return SerializerHelper.ToEnum<sbyte, TEnum>(
                BinarySerializer.SharedSerializeContext
                    .GetOrGenerate<sbyte>()
                    .Deserialize(reader));
            
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(bool)))
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToBoolean(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(TEnum obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is bool value)
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }
}
