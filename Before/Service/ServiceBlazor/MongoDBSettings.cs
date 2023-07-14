using Before.Data;
using Before.Data.Models;

namespace Before.Service.ServiceBlazor
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
