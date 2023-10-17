using MongoDB.Driver;
using TDDProject.Models;

namespace TDDProject.MongoDB;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoCollection<User> _collections;
    public MongoDBContext(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        _collections = _database.GetCollection<User>(collectionName);
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
