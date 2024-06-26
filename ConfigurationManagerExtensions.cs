using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongodbConfiguration;

public static class ConfigurationManagerExtensions
{
    public static ConfigurationManager AddMongoDbConfiguration(this ConfigurationManager manager, MongoClientSettings mongoSettings, string database, string collection)
    {
        ((IConfigurationBuilder) manager).Add(new MongoConfigurationSource(mongoSettings, database, collection));
        
        return manager;
    }
}