using Microsoft.Extensions.Configuration;
using MongodbConfiguration.Options;

namespace MongodbConfiguration;

public static class ConfigurationManagerExtensions
{
    public static ConfigurationManager AddMongoDbConfiguration(this ConfigurationManager manager, MongoDbConfigurationOptions options)
    {
        ((IConfigurationBuilder) manager).Add(new MongoConfigurationSource(options));
        
        return manager;
    }
}