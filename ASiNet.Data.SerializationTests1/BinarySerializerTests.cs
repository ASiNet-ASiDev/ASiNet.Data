using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Attributes;

namespace ASiNet.Data.Serialization.Tests;

[TestClass()]
public class BinarySerializerTests
{
    [TestMethod()]
    public void SerializeObjectAndBaseTypesTest()
    {
        var val = new TestObject();

        var l = BinarySerializer.GetSize(val);

        var buff = new byte[l];

        var lRes = BinarySerializer.Serialize(val, buff);

        var res = BinarySerializer.Deserialize<TestObject>(buff);

        Assert.IsNotNull(res);

        Assert.AreEqual(l, lRes);

        Assert.AreEqual(val.Str, res.Str);
        Assert.AreEqual(val.A, res.A);
        Assert.AreEqual(val.A1, res.A1);
        Assert.AreEqual(val.B, res.B);
        Assert.AreEqual(val.B1, res.B1);
        Assert.AreEqual(val.C, res.C);
        Assert.AreEqual(val.C1, res.C1);
        Assert.AreEqual(val.D, res.D);
        Assert.AreEqual(val.D1, res.D1);
        Assert.AreEqual(val.Ch, res.Ch);
        Assert.AreEqual(val.E, res.E);
        Assert.AreEqual(val.E1, res.E1);
        Assert.AreEqual(val.Dt, res.Dt);
        Assert.AreEqual(val.Gd, res.Gd);
        Assert.AreEqual(val.Ts, res.Ts);
        Assert.AreEqual(val.Bl, res.Bl);

    }

    [TestMethod()]
    public void SerializeStructAndBaseTypesTest()
    {
        var val = new TestStruct();

        var l = BinarySerializer.GetSize(val);

        var buff = new byte[l];

        var lRes = BinarySerializer.Serialize(val, buff);

        var res = BinarySerializer.Deserialize<TestStruct>(buff);

        Assert.IsNotNull(res);

        Assert.AreEqual(l, lRes);

        Assert.AreEqual(val.A, res.A);
        Assert.AreEqual(val.A1, res.A1);
        Assert.AreEqual(val.B, res.B);
        Assert.AreEqual(val.B1, res.B1);
        Assert.AreEqual(val.C, res.C);
        Assert.AreEqual(val.C1, res.C1);
        Assert.AreEqual(val.D, res.D);
        Assert.AreEqual(val.D1, res.D1);
        Assert.AreEqual(val.Ch, res.Ch);
        Assert.AreEqual(val.E, res.E);
        Assert.AreEqual(val.E1, res.E1);
        Assert.AreEqual(val.Dt, res.Dt);
        Assert.AreEqual(val.Gd, res.Gd);
        Assert.AreEqual(val.Ts, res.Ts);
        Assert.AreEqual(val.Bl, res.Bl);

    }

    [TestMethod()]
    public void SerializeArraysAndBaseTypesTest()
    {
        var val = new ArraysTest();

        var l = BinarySerializer.GetSize(val);

        var buff = new byte[l];

        var lRes = BinarySerializer.Serialize(val, buff);

        var res = BinarySerializer.Deserialize<ArraysTest>(buff);

        Assert.IsNotNull(res);

        Assert.AreEqual(l, lRes);

        Assert.IsTrue(val.A.SequenceEqual(res.A));
        Assert.IsTrue(val.B.SequenceEqual(res.B));
        Assert.IsTrue(val.C.SequenceEqual(res.C));
        Assert.IsTrue(val.D.SequenceEqual(res.D));
        Assert.IsTrue(val.E.SequenceEqual(res.E));
        Assert.IsTrue(val.A1.SequenceEqual(res.A1));
        Assert.IsTrue(val.B1.SequenceEqual(res.B1));
        Assert.IsTrue(val.C1.SequenceEqual(res.C1));
        Assert.IsTrue(val.D1.SequenceEqual(res.D1));
        Assert.IsTrue(val.E1.SequenceEqual(res.E1));
        Assert.IsTrue(val.Ch.SequenceEqual(res.Ch));
        Assert.IsTrue(val.Str.SequenceEqual(res.Str));
        Assert.IsTrue(val.Dt.SequenceEqual(res.Dt));
        Assert.IsTrue(val.Gd.SequenceEqual(res.Gd));
        Assert.IsTrue(val.Bl.SequenceEqual(res.Bl));
        Assert.IsTrue(val.Ts.SequenceEqual(res.Ts));

    }

    [TestMethod()]
    public void SerializeIARGTest()
    {
        IARGInterface val = new IARGTest<int>()
        { 
            A = 10, 
            Iar = new IARGTest<char>() 
            { 
                A = 'a'    
            },
            IarArr = [
                new IARGTest<int>() { A = 321 },
                new IARGTest<double>() { A = 10.55d },
                new IARGTest<Guid>() { A = Guid.NewGuid() },
                ]    
        };

        var l = BinarySerializer.GetSize(val);

        var buff = new byte[l];

        var lRes = BinarySerializer.Serialize(val, buff);

        var res = BinarySerializer.Deserialize<IARGInterface>(buff);

        Assert.IsNotNull(res);

        Assert.AreEqual(l, lRes);

        Assert.AreEqual(((IARGTest<int>)val).A, ((IARGTest<int>)res).A);
        Assert.AreEqual((((IARGTest<int>)val).Iar as IARGTest<char>).A, (((IARGTest<int>)res).Iar as IARGTest<char>).A);

        foreach (var item in ((IARGTest<int>)val).IarArr.Zip(((IARGTest<int>)res).IarArr))
        {
            Assert.IsTrue(item.First.Compare(item.Second));
        }

    }
}


public class TestObject
{
    public int A { get; set; } = -1;

    public uint A1 { get; set; } = 1;

    public short B { get; set; } = -10;

    public ushort B1 { get; set; } = 10;

    public long C { get; set; } = -80;

    public ulong C1 { get; set; } = 80;

    public double D { get; set; } = 1.5f;

    public float D1 { get; set; } = 4.5f;

    public char Ch { get; set; } = 'a';

    public byte E { get; set; } = 70;

    public sbyte E1 { get; set; } = -70;

    public DateTime Dt { get; set; } = DateTime.Now;

    public Guid Gd { get; set; } = Guid.NewGuid();

    public TimeSpan Ts { get; set; } = TimeSpan.FromSeconds(60);

    public bool Bl { get; set; } = true;

    public string Str { get; set; } = "Hello, World";

    [StringEncoding(EncodingType.UTF8)]
    public string StrUTF8 { get; set; } = "Hello, World";

    [StringEncoding(EncodingType.UTF32)]
    public string StrUTF32 { get; set; } = "Hello, World";

    [StringEncoding(EncodingType.Unicode)]
    public string StrUnicode { get; set; } = "Hello, World";

    [StringEncoding(EncodingType.ASCII)]
    public string StrASCII { get; set; } = "Hello, World";

    [StringEncoding(EncodingType.Latian1)]
    public string StrLatan1 { get; set; } = "Hello, World";
}

public struct TestStruct
{
    public TestStruct()
    {
    }

    public int A { get; set; } = -1;

    public uint A1 { get; set; } = 1;

    public short B { get; set; } = -10;

    public ushort B1 { get; set; } = 10;

    public long C { get; set; } = -80;

    public ulong C1 { get; set; } = 80;

    public double D { get; set; } = 1.5f;

    public float D1 { get; set; } = 4.5f;

    public char Ch { get; set; } = 'a';

    public byte E { get; set; } = 70;

    public sbyte E1 { get; set; } = -70;

    public DateTime Dt { get; set; } = DateTime.Now;

    public Guid Gd { get; set; } = Guid.NewGuid();

    public TimeSpan Ts { get; set; } = TimeSpan.FromSeconds(60);

    public bool Bl { get; set; } = true;
}

public class ArraysTest
{
    public int[] A { get; set; } = [-1];

    public uint[] A1 { get; set; } = [1];

    public short[] B { get; set; } = [-10];

    public ushort[] B1 { get; set; } = [10];

    public long[] C { get; set; } = [-80];

    public ulong[] C1 { get; set; } = [80];

    public double[] D { get; set; } = [1.5f];

    public float[] D1 { get; set; } = [4.5f];

    public char[] Ch { get; set; } = ['a'];

    public byte[] E { get; set; } = [70];

    public sbyte[] E1 { get; set; } = [-70];

    public DateTime[] Dt { get; set; } = [DateTime.Now];

    public Guid[] Gd { get; set; } = [Guid.NewGuid()];

    public TimeSpan[] Ts { get; set; } = [TimeSpan.FromSeconds(60)];

    public bool[] Bl { get; set; } = [true];

    public string[] Str { get; set; } = ["Hello, World"];
}

public interface IARGInterface
{ 
    public bool Compare(IARGInterface aRGInterface);    
}

public class IARGTest<T> : IARGInterface where T : IComparable<T>
{
    public T A { get; set; }
    public IARGInterface Iar { get; set; }

    public IARGInterface[] IarArr { get; set; }

    public bool Compare(IARGInterface aRGInterface)
    {
        if(aRGInterface.GetType().GetGenericArguments().First() == typeof(T))
        {
            var d = (IARGTest<T>)aRGInterface;

            return d.A.CompareTo(A) == 0;
        }
        return false;
    }
}