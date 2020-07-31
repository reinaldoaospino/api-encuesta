using System.Threading.Tasks;

namespace infraestructure.Interfaces
{
    public interface IMongoService
    {
        Task Create<T>(string collectionName, T record);
    }
}
