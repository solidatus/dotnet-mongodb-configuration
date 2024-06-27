using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solidatus.Extensions.Configuration.MongoDb.Internal;

namespace Solidatus.Extensions.Configuration.MongoDb;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder builder, MongoClientSettings mongoSettings, string database, string collection)
    {
        builder.Add(new MongoConfigurationSource(mongoSettings, database, collection));
        
        return builder;
    }
}