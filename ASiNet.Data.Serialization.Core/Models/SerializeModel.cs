using System;
using ASiNet.Data.Serialization.Hash;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models
{
    public abstract class SerializeModel<T> : ISerializeModel<T>
    {
        public long TypeHash => _typeHash.Value.Hash;
        public byte[] TypeHashBytes => _typeHash.Value.BytesHash;

        public Type ObjType => _objType.Value;

        private readonly Lazy<Type> _objType = new Lazy<Type>(() => typeof(T));

        private readonly Lazy<(byte[] BytesHash, long Hash)> _typeHash = new Lazy<(byte[] BytesHash, long Hash)>(() =>
        {
            var hash = PolynomialHasher.Shared.CalculateHash(typeof(T).FullName ?? typeof(T).Name);
            var bytes = BitConverter.GetBytes(hash);
            return (bytes, hash);
        });

        public abstract void Serialize(T obj, in ISerializeWriter writer);

        public abstract T Deserialize(in ISerializeReader reader);

        public abstract int ObjectSerializedSize(object obj);
        
        public abstract void SerializeObject(object obj, in ISerializeWriter writer);

        public abstract object DeserializeToObject(in ISerializeReader reader);

        public abstract int ObjectSerializedSize(T obj);
    }
}