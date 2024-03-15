using System.Text;

namespace ASiNet.Data.Serialization.Interfaces;
public interface ISerializeStringModel
{
    public void Serialize(string? obj, in ISerializeWriter writer, Encoding encoding);

    public string? Deserialize(in ISerializeReader reader, Encoding encoding);

    public int ObjectSerializedSize(string? obj, Encoding encoding);
}
