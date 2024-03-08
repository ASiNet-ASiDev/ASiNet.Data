using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models.Arrays.Unsafe;

namespace ASiNet.Data.Serialization.Models.Arrays;

file static class ArrayHelper
{
    public static bool IsNullArray<T>(T obj, ISerializeWriter writer)
    {
        if (obj is null)
        {
            writer.WriteByte(0);
            return true;
        }
        return false;
    }

    public static int ReadLength(ISerializeReader reader)
    {
        var lBuff = (stackalloc byte[sizeof(int)]);
        reader.ReadBytes(lBuff);
        return BitConverter.ToInt32(lBuff);
    }

    public static void WriteLength(int length, ISerializeWriter writer)
    {
        var l = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(l, length);
        writer.WriteBytes(l);
    }

    public static void ThrowIsIndexOutOfRange(int bytesSize, ISerializeWriter writer)
    {
        if (!writer.CanWriteSize(bytesSize + sizeof(byte) + sizeof(int)))
            throw new Exception();
    }

    public static void BlokcCopyElementsUnmanaged<T>(T[] src, int bytesSize, ISerializeWriter writer) where T : unmanaged
    {
        var buffer = new byte[bytesSize];
        Buffer.BlockCopy(src, 0, buffer, 0, buffer.Length);

        writer.WriteBytes(buffer);
    }

    public static int GetArraySize(int? arrayLength, int? elementSize, bool isNull) =>
        !isNull ? arrayLength!.Value * elementSize!.Value + sizeof(int) + sizeof(byte)
        : sizeof(byte);

}

public class Int32ArrayModel : BaseSerializeModel<int[]>
{
    public override void Serialize(int[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;
            
        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(int);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override int[]? Deserialize(ISerializeReader reader)
    {
        if(reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(int)];
        reader.ReadBytes(bytes);

        return bytes.AsInt32Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((int[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(int[]? obj) => 
        ArrayHelper.GetArraySize(obj?.Length, sizeof(int), obj is null);
    
}

public class UInt32ArrayModel : BaseSerializeModel<uint[]>
{
    public override void Serialize(uint[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(uint);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override uint[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(int)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt32Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((uint[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(uint[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(uint), obj is null);
}

public class Int16ArrayModel : BaseSerializeModel<short[]>
{
    public override void Serialize(short[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(short);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override short[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(short)];
        reader.ReadBytes(bytes);

        return bytes.AsInt16Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((short[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(short[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(short), obj is null);
}

public class UInt16ArrayModel : BaseSerializeModel<ushort[]>
{
    public override void Serialize(ushort[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(ushort);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override ushort[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(ushort)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt16Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((ushort[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(ushort[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(ushort), obj is null);
}

public class Int64ArrayModel : BaseSerializeModel<long[]>
{
    public override void Serialize(long[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(long);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override long[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(long)];
        reader.ReadBytes(bytes);

        return bytes.AsInt64Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((long[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(long[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(long), obj is null);
}

public class UInt64ArrayModel : BaseSerializeModel<ulong[]>
{
    public override void Serialize(ulong[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(ulong);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override ulong[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(ulong)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt64Array();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((ulong[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(ulong[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(ulong), obj is null);
}

public class CharArrayModel : BaseSerializeModel<char[]>
{
    public override void Serialize(char[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(char);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override char[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(char)];
        reader.ReadBytes(bytes);

        return bytes.AsCharArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((char[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(char[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(char), obj is null);
}

public class BooleanArrayModel : BaseSerializeModel<bool[]>
{
    public override void Serialize(bool[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(bool);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override bool[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(bool)];
        reader.ReadBytes(bytes);

        return bytes.AsBooleanArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((bool[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(bool[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(bool), obj is null);
}

public class DateTimeArrayModel : BaseSerializeModel<DateTime[]>
{
    public override void Serialize(DateTime[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(long);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);

        var dist = new DateTime[obj.Length];
        obj.CopyTo(dist, 0);

        writer.WriteBytes(dist.AsByteArray());
    }

    public override DateTime[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(long)];
        reader.ReadBytes(bytes);

        return bytes.AsDateTimeArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((DateTime[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(DateTime[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(long), obj is null);
}

public class DoubleArrayModel : BaseSerializeModel<double[]>
{
    public override void Serialize(double[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(double);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override double[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(double)];
        reader.ReadBytes(bytes);

        return bytes.AsDoubleArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((double[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(double[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(double), obj is null);
}

public class SingleArrayModel : BaseSerializeModel<float[]>
{
    public override void Serialize(float[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(float);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override float[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(float)];
        reader.ReadBytes(bytes);

        return bytes.AsSingleArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((float[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(float[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(float), obj is null);
}

public class GuidArrayModel : BaseSerializeModel<Guid[]>
{
    public override void Serialize(Guid[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(decimal);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);

        var dist = new Guid[obj.Length];
        obj.CopyTo(dist, 0);

        writer.WriteBytes(dist.AsByteArray());
    }

    public override Guid[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(decimal)];
        reader.ReadBytes(bytes);

        return bytes.AsGuidArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((Guid[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(Guid[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(decimal), obj is null);
}

public class SByteArrayModel : BaseSerializeModel<sbyte[]>
{
    public override void Serialize(sbyte[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(sbyte);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override sbyte[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(sbyte)];
        reader.ReadBytes(bytes);

        return bytes.AsSByteArray();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((sbyte[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(sbyte[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(sbyte), obj is null);
}

public class ByteArrayModel : BaseSerializeModel<byte[]>
{
    public override void Serialize(byte[]? obj, ISerializeWriter writer)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length;

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override byte[]? Deserialize(ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length];
        reader.ReadBytes(bytes);

        return bytes;
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((byte[]?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override int ObjectSerializedSize(byte[]? obj) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(byte), obj is null);
}