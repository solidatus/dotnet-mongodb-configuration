using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Solidatus.MongoDb.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddMongoDbConfiguration(this IConfigurationBuilder builder, MongoClientSettings mongoSettings, string database, string collection)
    {
        builder.Add(new MongoConfigurationSource(mongoSettings, database, collection));
        
        return builder;
    }
}