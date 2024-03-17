using System;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Interfaces
{
    public interface ISerializerContext
    {
        void AddModel(ISerializeModel model);

        void AddModel<T>(SerializeModel<T> model);


        bool RemoveModel(ISerializeModel model);

        bool RemoveModel<T>(SerializeModel<T> model);


        ISerializeModel GenerateModel(Type type);

        SerializeModel<T> GenerateModel<T>();


        ISerializeModel GetOrGenerate(Type type);

        SerializeModel<T> GetOrGenerate<T>();

        ISerializeModel? GetModel(Type type);

        SerializeModel<T>? GetModel<T>();


        ISerializeModel GetOrGenerateByHash(long hash);
        ISerializeModel GetModelByHash(long hash);


        void AddGegerator(IModelsGenerator generator);

        bool RemoveGegerator(IModelsGenerator Generator);

        bool ContainsModel(Type type);

        bool ContainsModel<T>();
    }

}