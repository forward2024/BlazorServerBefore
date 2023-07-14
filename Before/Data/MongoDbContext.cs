using Before.Data.Models;
using MongoDB.Driver;

namespace Before.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<MongoDBProduct> MongoDBProducts
        {
            get
            {
                return database.GetCollection<MongoDBProduct>("MongoDBProduct");
            }
        }
    }
}
