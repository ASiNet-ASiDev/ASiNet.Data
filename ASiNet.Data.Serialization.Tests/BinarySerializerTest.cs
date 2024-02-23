using System;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASiNet.Data.Serialization.Tests;

[TestClass]
[TestSubject(typeof(BinarySerializer))]
public class BinarySerializerTest
{
    [TestMethod]
    public void BaseTypesDefaultNotAll()
    {
        Assert.IsTrue( Test<short>(sizeof(short), short.MaxValue) );
        Assert.IsTrue( Test<int>(sizeof(int),int.MaxValue) );
        Assert.IsTrue( Test<long>(sizeof(long), long.MaxValue) );
        
        Assert.IsTrue( Test<ushort>(sizeof(ushort), ushort.MaxValue) );
        Assert.IsTrue( Test<uint>(sizeof(uint),uint.MaxValue) );
        Assert.IsTrue( Test<ulong>(sizeof(ulong), ulong.MaxValue) );
        
        Assert.IsTrue( Test<byte>(sizeof(byte), byte.MaxValue) );
        Assert.IsTrue( Test<sbyte>(sizeof(sbyte),sbyte.MaxValue) );

        Assert.IsTrue( Test<float>(sizeof(float),float.MaxValue) );
        Assert.IsTrue( Test<double>(sizeof(double),double.MaxValue) );
    }
    
    private bool Test<T>(int size, T val)
    {
        var b1 = new byte[size];
        BinarySerializer.Serialize(val, (ArrayWriter)b1);
        return BinarySerializer.Deserialize<T>((ArrayReader)b1).Equals(val);
    }
}