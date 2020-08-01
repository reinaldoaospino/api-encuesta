using infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace infraestructure.Interfaces
{
    public interface IMongoService
    {
        Task<IEnumerable<T>> Get<T>(string collectionName);
        Task Create<T>(string collectionName, T record);
        Task Update<T>(string collectionName,string id, T record);
        Task Delete<T>(string collectionName, string id);
    }
}
