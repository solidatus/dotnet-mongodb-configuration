using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Solidatus.Extensions.Configuration.MongoDb.Internal;

internal sealed class MongoConfigurationSource(MongoClientSettings mongoSettings, string databaseName, string collectionName) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var collection = MongoProvider.GetCollection(mongoSettings, databaseName, collectionName);

        return MongoConfigurationProvider.Create(collection);
    }
}