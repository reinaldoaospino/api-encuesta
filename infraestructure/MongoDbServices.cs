using Domain.Abstations.Infraestructure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure
{
    public class MongoDbServices : IMongoDbServices
    {
        private IMongoDatabase _db;

        public MongoDbServices(string database)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(database);
        }

        public async Task Create<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }
    }
}
