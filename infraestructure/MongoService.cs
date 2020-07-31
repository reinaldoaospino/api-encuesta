using MongoDB.Driver;
using System.Threading.Tasks;
using infraestructure.Interfaces;

namespace infraestructure
{
    public class MongoService : IMongoService
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoClient _mongoClient;
   
        public MongoService(string database, IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _db = _mongoClient.GetDatabase(database);
        }

        public async Task Create<T>(string collectionName, T record)
        {
            var  collection = _db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(record);
        }
    }
}