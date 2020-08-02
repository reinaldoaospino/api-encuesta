using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infraestructure.Interfaces
{
    public interface IMongoService
    {
        Task<IEnumerable<T>> Get<T>(string collectionName);
        Task Create<T>(string collectionName, T record);
        Task Update<T>(string collectionName,string id, T record);
        Task Delete<T>(string collectionName, string id);
    }
}
