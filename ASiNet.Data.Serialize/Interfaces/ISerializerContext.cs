using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Interfaces;
public interface ISerializerContext
{
    public void AddModel(ISerializeModel model);

    public void AddModel<T>(SerializeModel<T> model);


    public bool RemoveModel(ISerializeModel model);

    public bool RemoveModel<T>(SerializeModel<T> model);


    public ISerializeModel GenerateModel(Type type);
    
    public SerializeModel<T> GenerateModel<T>();


    public ISerializeModel GetOrGenerate(Type type);

    public SerializeModel<T> GetOrGenerate<T>();

    public ISerializeModel? GetModel(Type type);

    public SerializeModel<T>? GetModel<T>();


    public ISerializeModel GetOrGenerateByHash(string hash);
    public ISerializeModel GetModelByHash(string hash);


    public void AddGegerator(Predicate<Type> Comparer, IModelsGenerator Generator);

    public bool RemoveGegerator(IModelsGenerator Generator);

    public bool ContainsModel(Type type);

    public bool ContainsModel<T>();
}
