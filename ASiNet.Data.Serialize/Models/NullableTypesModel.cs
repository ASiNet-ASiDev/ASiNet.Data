using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Reflection;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;
public class NullableTypesModel<T> : BaseSerializeModel<T?>
{
    private Lazy<Type> _underlyingType = new(() => Nullable.GetUnderlyingType(typeof(T))!);

    private Lazy<ISerializeModel> _underlyingSerializeModel = 
        new(() => BinarySerializer.SharedSerializeContext.GetOrGenerate(Nullable.GetUnderlyingType(typeof(T))!));
    
    private Lazy<Func<T, object>> NullableGetValueLambda = new(() =>
    {
        var instParam = Expression.Parameter(typeof(T));

        var resultProp = Expression.Convert(Expression.Property(instParam, nameof(Nullable<byte>.Value)), typeof(object));

        return Expression.Lambda<Func<T, object>>(resultProp, instParam).Compile();
    });
    
    private Lazy<Func<object, T>> CreateNullableLambda = new(() =>
    {
        var instParam = Expression.Parameter(typeof(object));

        var type = Nullable.GetUnderlyingType(typeof(T))!;
        
        var body = Expression.New(
            typeof(T).GetConstructor(BindingFlags.Public|BindingFlags.Instance, new []{type}), 
            Expression.Convert(instParam, type));    
        
        return Expression.Lambda<Func<object, T>>(body, instParam).Compile();
    });
    
    
    public override void Serialize(T obj, ISerializeWriter writer)
    {
        byte val = (byte)(obj is null ? 0 : 1);
        writer.WriteByte(val);
        
        if (val == 1)
        {
            var instance = NullableGetValueLambda.Value(obj);
            _underlyingSerializeModel.Value.SerializeObject(instance, writer);
        }
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        Serialize((T)obj, writer);
    }

    public override T? Deserialize(ISerializeReader reader)
    {
        byte val = reader.ReadByte();
        if (val == 0)
            return default;
        
        if (val == 1)
        {
            var instance = _underlyingSerializeModel.Value.DeserializeToObject(reader);
            return CreateNullableLambda.Value(instance);
        }

        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}
