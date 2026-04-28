using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Solidatus.Extensions.Configuration.MongoDb.Internal;

internal sealed class MongoConfigurationSource(MongoClientSettings mongoSettings, string databaseName, string collectionName) : IConfigurationSource
{
    internal static MongoConfigurationProvider Provider { get; set; }
    
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var collection = MongoProvider.GetCollection(mongoSettings, databaseName, collectionName);

        Provider = new MongoConfigurationProvider(collection);
        
        return Provider;
    }
}