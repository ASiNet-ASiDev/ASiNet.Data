using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class EnumModel<TEnum> : BaseSerializeModel<TEnum>
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
        return Deserialize(reader);
        throw new Exception();
    }

    public override void Serialize(TEnum obj, ISerializeWriter writer)
    {
        var etype = typeof(TEnum).GetEnumUnderlyingType();
        
        if (etype == typeof(byte))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<byte>()
                .Serialize((byte)(object)obj, writer);
            return;
        }

        if (etype == typeof(int))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<int>()
                .Serialize((int)(object)obj, writer);
            return;
        }
        
        if (etype == typeof(short))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<short>()
                .Serialize((short)(object)obj, writer);
            return;
        }

        if (etype == typeof(long))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<long>()
                .Serialize((long)(object)obj, writer);
            return;
        }

        if (etype == typeof(sbyte))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<byte>()
                .Serialize((byte)(object)obj, writer);
            return;
        }

        if (etype == typeof(uint))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<uint>()
                .Serialize((uint)(object)obj, writer);
            return;
        }
        
        if (etype == typeof(ushort))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<ushort>()
                .Serialize((ushort)(object)obj, writer);
            return;
        }

        if (etype == typeof(ulong))
        {
            BinarySerializer.SharedSerializeContext
                .GetOrGenerate<ulong>()
                .Serialize((ulong)(object)obj, writer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is TEnum value)
        {
            Serialize(value, writer);
        }
        throw new Exception();
    }
}
