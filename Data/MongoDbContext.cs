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

        public IMongoCollection<Hotel> hotel => _database.GetCollection<Hotel>("hotel");
        public IMongoCollection<User> user => _database.GetCollection<User>("user");
    }
}
