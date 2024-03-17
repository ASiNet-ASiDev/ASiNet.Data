using System;

namespace ASiNet.Data.Serialization.Interfaces
{
    /// <summary>
    /// An interface that provides an API for creating serializer models.
    /// <para/>
    /// To create your own models, it is better to use <see cref="Models.SerializeModelBase{T}"/>
    /// </summary>
    public interface ISerializeModel
    {

        long TypeHash { get; }
        byte[] TypeHashBytes { get; }
        /// <summary>
        /// The type of the object of the current model
        /// </summary>
        Type ObjType { get; }

        int ObjectSerializedSize(object obj);

        /// <summary>
        /// Writes an object as byte data.
        /// </summary>
        /// <param name="obj"> The object being recorded. </param>
        /// <param name="writer"> The byte space where the object is written to. </param>
        void SerializeObject(object obj, in ISerializeWriter writer);

        /// <summary>
        /// Reads an object from byte data.
        /// </summary>
        /// <param name="reader"> Byte space from where the object is read. </param>
        /// <returns></returns>
        object DeserializeToObject(in ISerializeReader reader);
    }


    public interface ISerializeModel<T> : ISerializeModel
    {
        /// <summary>
        /// Writes an object as byte data.
        /// </summary>
        /// <param name="obj"> The object being recorded. </param>
        /// <param name="writer"> The byte space where the object is written to. </param>
        void Serialize(T obj, in ISerializeWriter writer);

        /// <summary>
        /// Reads an object from byte data.
        /// </summary>
        /// <param name="reader"> Byte space from where the object is read. </param>
        /// <returns></returns>
        T Deserialize(in ISerializeReader reader);


        int ObjectSerializedSize(T obj);
    }

}