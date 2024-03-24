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
        if (!writer.CanWriteSize(bytesSize + sizeof(int)))
            throw new IndexOutOfRangeException();
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

public class Int32ArrayModel : SerializeModelBase<int[]>
{
    public override void Serialize(int[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(int);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override int[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(int)];
        reader.ReadBytes(bytes);

        return bytes.AsInt32Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((int[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(int[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(int), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((int[]?)obj, context);
}

public class UInt32ArrayModel : SerializeModelBase<uint[]>
{
    public override void Serialize(uint[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(uint);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override uint[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(uint)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt32Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((uint[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(uint[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(uint), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((uint[]?)obj, context);
}

public class Int16ArrayModel : SerializeModelBase<short[]>
{
    public override void Serialize(short[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(short);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override short[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(short)];
        reader.ReadBytes(bytes);

        return bytes.AsInt16Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((short[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(short[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(short), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((short[]?)obj, context);
}

public class UInt16ArrayModel : SerializeModelBase<ushort[]>
{
    public override void Serialize(ushort[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(ushort);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override ushort[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(ushort)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt16Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((ushort[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(ushort[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(ushort), obj is null);


    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((ushort[]?)obj, context);
}

public class Int64ArrayModel : SerializeModelBase<long[]>
{
    public override void Serialize(long[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(long);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override long[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(long)];
        reader.ReadBytes(bytes);

        return bytes.AsInt64Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((long[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(long[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(long), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((long[]?)obj, context);
}

public class UInt64ArrayModel : SerializeModelBase<ulong[]>
{
    public override void Serialize(ulong[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(ulong);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override ulong[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(ulong)];
        reader.ReadBytes(bytes);

        return bytes.AsUInt64Array();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((ulong[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(ulong[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(ulong), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((ulong[]?)obj, context);
}

public class CharArrayModel : SerializeModelBase<char[]>
{
    public override void Serialize(char[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(char);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override char[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(char)];
        reader.ReadBytes(bytes);

        return bytes.AsCharArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((char[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(char[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(char), obj is null);


    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((char[]?)obj, context);
}

public class BooleanArrayModel : SerializeModelBase<bool[]>
{
    public override void Serialize(bool[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(bool);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override bool[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(bool)];
        reader.ReadBytes(bytes);

        return bytes.AsBooleanArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((bool[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(bool[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(bool), obj is null);


    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((bool[]?)obj, context);
}

public class DateTimeArrayModel : SerializeModelBase<DateTime[]>
{
    public override void Serialize(DateTime[]? obj, in ISerializeWriter writer, ISerializerContext context)
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

    public override DateTime[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(long)];
        reader.ReadBytes(bytes);

        return bytes.AsDateTimeArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((DateTime[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(DateTime[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(long), obj is null);


    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((DateTime[]?)obj, context);
}

public class DoubleArrayModel : SerializeModelBase<double[]>
{
    public override void Serialize(double[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(double);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override double[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(double)];
        reader.ReadBytes(bytes);

        return bytes.AsDoubleArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((double[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(double[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(double), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((double[]?)obj, context);
}

public class SingleArrayModel : SerializeModelBase<float[]>
{
    public override void Serialize(float[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(float);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override float[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(float)];
        reader.ReadBytes(bytes);

        return bytes.AsSingleArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((float[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(float[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(float), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((float[]?)obj, context);
}

public class GuidArrayModel : SerializeModelBase<Guid[]>
{
    public override void Serialize(Guid[]? obj, in ISerializeWriter writer, ISerializerContext context)
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

    public override Guid[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(decimal)];
        reader.ReadBytes(bytes);

        return bytes.AsGuidArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((Guid[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(Guid[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(decimal), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((Guid[]?)obj, context);
}

public class SByteArrayModel : SerializeModelBase<sbyte[]>
{
    public override void Serialize(sbyte[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length * sizeof(sbyte);

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override sbyte[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length * sizeof(sbyte)];
        reader.ReadBytes(bytes);

        return bytes.AsSByteArray();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((sbyte[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(sbyte[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(sbyte), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((sbyte[]?)obj, context);
}

public class ByteArrayModel : SerializeModelBase<byte[]>
{
    public override void Serialize(byte[]? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (ArrayHelper.IsNullArray(obj, writer))
            return;

        writer.WriteByte(1);

        var arrBytesLength = obj!.Length;

        ArrayHelper.ThrowIsIndexOutOfRange(arrBytesLength, writer);
        ArrayHelper.WriteLength(obj.Length, writer);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, writer);
    }

    public override byte[]? Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.ReadByte() == 0)
            return null;

        var length = ArrayHelper.ReadLength(reader);

        var bytes = new byte[length];
        reader.ReadBytes(bytes);

        return bytes;
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((byte[]?)obj, writer, context);

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override int ObjectSerializedSize(byte[]? obj, ISerializerContext context) =>
        ArrayHelper.GetArraySize(obj?.Length, sizeof(byte), obj is null);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => 
        ObjectSerializedSize((byte[]?)obj, context);
}