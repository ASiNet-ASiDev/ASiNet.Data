using System;
using System.Collections.Generic;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using JetBrains.Annotations;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASiNet.Data.Serialization.Tests;

[TestClass]
[TestSubject(typeof(ArrayTests))]
public class ArrayTests
{
    class A
    {
        public int[] Arr { get; set; }
        public int[] Brr { get; set; }
        public int[] Drr { get; set; }
    }
    class B
    {
        public int[] Drr { get; set; }
        public int[] Arr { get; set; }
        public int[] Brr { get; set; }
    }
    [TestMethod]
    public void ClassPropertyOrderAlphabet()
    {
        var a = new A()
        {
            Arr = RandArr(9999),
            Brr = RandArr(999),
            Drr = RandArr(99),
        };
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (Interfaces.ISerializeWriter)(ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<A>((Interfaces.ISerializeReader)(ArrayReader)buffer);
        Assert.IsTrue( EqualArr(a.Arr, a2.Arr) && EqualArr(a.Brr, a2.Brr) && EqualArr(a.Drr, a2.Drr) );
    }
    [TestMethod]
    public void ClassPropertOrderRand()
    {
        var a = new B()
        {
            Arr = RandArr(9999),
            Brr = RandArr(999),
            Drr = RandArr(99),
        };
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (Interfaces.ISerializeWriter)(ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<B>((Interfaces.ISerializeReader)(ArrayReader)buffer);
        Assert.IsTrue( EqualArr(a.Arr, a2.Arr) && EqualArr(a.Brr, a2.Brr) && EqualArr(a.Drr, a2.Drr) );
    }

    [TestMethod]
    public void DictionaryTest()
    {
        var a = new Dictionary<int, string>();
        a.TryAdd(1, "hello");
        a.TryAdd(2, "world");
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (Interfaces.ISerializeWriter)(ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<Dictionary<int, string>>((Interfaces.ISerializeReader)(ArrayReader)buffer);

        var res = a.TryGetValue(1, out var a1res1) && a.TryGetValue(2, out var a1res2) 
            && a2.TryGetValue(1, out var a2res1) && a2.TryGetValue(2, out var a2res2) &&
            a1res1 == a2res1 && a1res2 == a2res2;
        Assert.IsTrue(res);
    }


    class A2
    {
        public int[] Arr { get; set; }
        public int[]? Brr { get; set; }
        public int[] Drr { get; set; }
        public int[]? Err { get; set; }

        public A3[] ArrObjects { get; set; }

        public A3[] ArrObjectsNull { get; set; }
    }
    class B2
    {
        public int[]? Err { get; set; }
        public int[] Drr { get; set; }
        public int[]? Arr { get; set; }
        public int[] Brr { get; set; }
    }

    class A3
    {
        public int A { get; set; }

        public int B { get; set; }
    }

    [TestMethod]
    public void ClassNullablePropertOrderAlphabet()
    {
        var a = new A2()
        {
            Arr = RandArr(9999),
            Brr = RandArr(999),
            Drr = RandArr(99),
            Err = null,
            ArrObjects = [new(), new()],
        };
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (Interfaces.ISerializeWriter)(ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<A2>((Interfaces.ISerializeReader)(ArrayReader)buffer);
        Assert.IsTrue( EqualArr(a.Arr, a2.Arr) && EqualArr(a.Brr, a2.Brr) && EqualArr(a.Drr, a2.Drr) && a.Err == a2.Err && a.ArrObjects.Length == a2.ArrObjects.Length);
    }
    [TestMethod]
    public void ClassNullablePropertOrderRand()
    {
        var a = new B2()
        {
            Arr = RandArr(9999),
            Brr = RandArr(999),
            Drr = RandArr(99),
            Err = null,
        };
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (Interfaces.ISerializeWriter)(ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<B2>((Interfaces.ISerializeReader)(ArrayReader)buffer);
        Assert.IsTrue( EqualArr(a.Arr, a2.Arr) && EqualArr(a.Brr, a2.Brr) && EqualArr(a.Drr, a2.Drr) && a.Err == a2.Err );
    }
    
    private bool EqualArr(int[] a, int[] b)
    {
        if (a.Length != b.Length)
            return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                return false;
        }
        return true;
    }

    private int[] RandArr(int size)
    {
        int[] res = new int[size];
        for (int i = 0; i < size; i++)
            res[i] = Random.Shared.Next();
        return res;
    }
}