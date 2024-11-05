using Entities;
using MongoDB.Driver;

namespace Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionUri, string databaseName)
        {
            var client = new MongoClient(connectionUri);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<hotel> hotel => _database.GetCollection<hotel>("hotel");
    }
}
