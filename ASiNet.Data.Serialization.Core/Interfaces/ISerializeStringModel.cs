using System.Text;

namespace ASiNet.Data.Serialization.Interfaces
{
    public interface ISerializeStringModel
    {
        void Serialize(string obj, in ISerializeWriter writer, ISerializerContext context, Encoding encoding);

        string Deserialize(in ISerializeReader reader, ISerializerContext context, Encoding encoding);

        int ObjectSerializedSize(string obj, ISerializerContext context, Encoding encoding);
    }

}