namespace ASiNet.Data.Serialization.Models.Arrays.Unsafe;
public static class BooleanArrayConverter
{
    public static byte[] AsByteArray(this bool[] input)
    {
        var union = new BooleanUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static bool[] AsBooleanArray(this byte[] input)
    {
        var union = new BooleanUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<bool>(ArrayConverter.BOOLEAN_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(bool[] input, out byte[] converted)
    {
        var union = new BooleanUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsBooleanArray(byte[] input, out bool[] converted)
    {
        var union = new BooleanUnion { Bytes = input };
        union.Bytes.ToObjestsArray<bool>(ArrayConverter.BOOLEAN_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class CharArrayConverter
{
    public static byte[] AsByteArray(this char[] input)
    {
        var union = new CharUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static char[] AsCharArray(this byte[] input)
    {
        var union = new CharUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<char>(ArrayConverter.CHAR_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(char[] input, out byte[] converted)
    {
        var union = new CharUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsCharArray(byte[] input, out char[] converted)
    {
        var union = new CharUnion { Bytes = input };
        union.Bytes.ToObjestsArray<char>(ArrayConverter.CHAR_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class DateTimeArrayConverter
{
    public static byte[] AsByteArray(this DateTime[] input)
    {
        var union = new DateTimeUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static DateTime[] AsDateTimeArray(this byte[] input)
    {
        var union = new DateTimeUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<DateTime>(ArrayConverter.DATETIME_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(DateTime[] input, out byte[] converted)
    {
        var union = new DateTimeUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsDateTimeArray(byte[] input, out DateTime[] converted)
    {
        var union = new DateTimeUnion { Bytes = input };
        union.Bytes.ToObjestsArray<DateTime>(ArrayConverter.DATETIME_ARRAY_TYPE);
        converted = union.Objects!;
    }

}

public static class DoubleArrayConverter
{
    public static byte[] AsByteArray(this double[] input)
    {
        var union = new DoubleUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static double[] AsDoubleArray(this byte[] input)
    {
        var union = new DoubleUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<double>(ArrayConverter.DOUBLE_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(double[] input, out byte[] converted)
    {
        var union = new DoubleUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsDoubleArray(byte[] input, out double[] converted)
    {
        var union = new DoubleUnion { Bytes = input };
        union.Bytes.ToObjestsArray<double>(ArrayConverter.DOUBLE_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class SingleArrayConverter
{
    public static byte[] AsByteArray(this float[] input)
    {
        var union = new SingleUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static float[] AsSingleArray(this byte[] input)
    {
        var union = new SingleUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<float>(ArrayConverter.FLOAT_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(float[] input, out byte[] converted)
    {
        var union = new SingleUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsSingleArray(byte[] input, out float[] converted)
    {
        var union = new SingleUnion { Bytes = input };
        union.Bytes.ToObjestsArray<float>(ArrayConverter.FLOAT_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class GuidArrayConverter
{
    public static byte[] AsByteArray(this Guid[] input)
    {
        var union = new GuidUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static Guid[] AsGuidArray(this byte[] input)
    {
        var union = new GuidUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<Guid>(ArrayConverter.GUID_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(Guid[] input, out byte[] converted)
    {
        var union = new GuidUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsGuidArray(byte[] input, out Guid[] converted)
    {
        var union = new GuidUnion { Bytes = input };
        union.Bytes.ToObjestsArray<Guid>(ArrayConverter.GUID_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class Int16ArrayConverter
{
    public static byte[] AsByteArray(this short[] input)
    {
        var union = new Int16Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static short[] AsInt16Array(this byte[] input)
    {
        var union = new Int16Union { Bytes = input };
        union.Bytes!.ToObjestsArray<short>(ArrayConverter.INT16_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(short[] input, out byte[] converted)
    {
        var union = new Int16Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsInt16Array(byte[] input, out short[] converted)
    {
        var union = new Int16Union { Bytes = input };
        union.Bytes.ToObjestsArray<short>(ArrayConverter.INT16_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class UInt16ArrayConverter
{
    public static byte[] AsByteArray(this ushort[] input)
    {
        var union = new UInt16Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static ushort[] AsUInt16Array(this byte[] input)
    {
        var union = new UInt16Union { Bytes = input };
        union.Bytes!.ToObjestsArray<ushort>(ArrayConverter.UINT16_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(ushort[] input, out byte[] converted)
    {
        var union = new UInt16Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsUInt16Array(byte[] input, out ushort[] converted)
    {
        var union = new UInt16Union { Bytes = input };
        union.Bytes.ToObjestsArray<ushort>(ArrayConverter.UINT16_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class Int32ArrayConverter
{
    public static byte[] AsByteArray(this int[] input)
    {
        var union = new Int32Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static int[] AsInt32Array(this byte[] input)
    {
        var union = new Int32Union { Bytes = input };
        union.Bytes!.ToObjestsArray<int>(ArrayConverter.INT32_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(int[] input, out byte[] converted)
    {
        var union = new Int32Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsInt32Array(byte[] input, out int[] converted)
    {
        var union = new Int32Union { Bytes = input };
        union.Bytes.ToObjestsArray<int>(ArrayConverter.INT32_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class UInt32ArrayConverter
{
    public static byte[] AsByteArray(this uint[] input)
    {
        var union = new UInt32Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static uint[] AsUInt32Array(this byte[] input)
    {
        var union = new UInt32Union { Bytes = input };
        union.Bytes!.ToObjestsArray<uint>(ArrayConverter.UINT32_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(uint[] input, out byte[] converted)
    {
        var union = new UInt32Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsUInt32Array(byte[] input, out uint[] converted)
    {
        var union = new UInt32Union { Bytes = input };
        union.Bytes.ToObjestsArray<uint>(ArrayConverter.UINT32_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class Int64ArrayConverter
{
    public static byte[] AsByteArray(this long[] input)
    {
        var union = new Int64Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static long[] AsInt64Array(this byte[] input)
    {
        var union = new Int64Union { Bytes = input };
        union.Bytes!.ToObjestsArray<long>(ArrayConverter.INT64_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(long[] input, out byte[] converted)
    {
        var union = new Int64Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsInt64Array(byte[] input, out long[] converted)
    {
        var union = new Int64Union { Bytes = input };
        union.Bytes.ToObjestsArray<long>(ArrayConverter.INT64_ARRAY_TYPE);
        converted = union.Objects!;
    }
}


public static class UInt64ArrayConverter
{
    public static byte[] AsByteArray(this ulong[] input)
    {
        var union = new UInt64Union { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static ulong[] AsUInt64Array(this byte[] input)
    {
        var union = new UInt64Union { Bytes = input };
        union.Bytes!.ToObjestsArray<ulong>(ArrayConverter.UINT64_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(ulong[] input, out byte[] converted)
    {
        var union = new UInt64Union { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsUInt64Array(byte[] input, out ulong[] converted)
    {
        var union = new UInt64Union { Bytes = input };
        union.Bytes.ToObjestsArray<ulong>(ArrayConverter.UINT64_ARRAY_TYPE);
        converted = union.Objects!;
    }
}

public static class SByteArrayConverter
{
    public static byte[] AsByteArray(this sbyte[] input)
    {
        var union = new SByteUnion { Objects = input };
        union.Objects.ToBytesArray();
        return union.Bytes!;
    }

    public static sbyte[] AsSByteArray(this byte[] input)
    {
        var union = new SByteUnion { Bytes = input };
        union.Bytes!.ToObjestsArray<sbyte>(ArrayConverter.SBYTE_ARRAY_TYPE);
        return union.Objects!;
    }

    public static void AsByteArray(sbyte[] input, out byte[] converted)
    {
        var union = new SByteUnion { Objects = input };
        union.Objects.ToBytesArray();
        converted = union.Bytes!;
    }

    public static void AsSByteArray(byte[] input, out sbyte[] converted)
    {
        var union = new SByteUnion { Bytes = input };
        union.Bytes.ToObjestsArray<sbyte>(ArrayConverter.SBYTE_ARRAY_TYPE);
        converted = union.Objects!;
    }
}