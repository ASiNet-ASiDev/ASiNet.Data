using System.Text;

namespace ASiNet.Data.Serialization.Interfaces
{
    public interface ISerializeStringModel
    {
        void Serialize(string obj, in ISerializeWriter writer, Encoding encoding);

        string Deserialize(in ISerializeReader reader, Encoding encoding);

        int ObjectSerializedSize(string obj, Encoding encoding);
    }

}