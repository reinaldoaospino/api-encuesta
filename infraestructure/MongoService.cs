using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using infraestructure.Interfaces;
using System.Collections.Generic;

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

        public async Task<IEnumerable<T>> Get<T>(string collectionName)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var document = await collection.FindAsync(new BsonDocument());

            return document.ToEnumerable();
        }

        public async Task Create<T>(string collectionName, T record)
        {
            var collection = _db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(record);
        }

        public async Task Update<T>(string collectionName, string id, T record)
        {
            var collection = _db.GetCollection<T>(collectionName);

            await collection.ReplaceOneAsync(
                new BsonDocument("_id", id),
                record);
        }

        public async Task Delete<T>(string collectionName, string id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);
        }
    }
}