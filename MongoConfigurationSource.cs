using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongodbConfiguration;

public sealed class MongoConfigurationSource(MongoClientSettings mongoSettings, string database, string collection) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var client = new MongoClient(mongoSettings);

        return new MongoConfigurationProvider(client, database, collection);
    }
}