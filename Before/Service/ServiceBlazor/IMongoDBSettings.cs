using Before.Data.Models;

namespace Before.Service.ServiceBlazor
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
