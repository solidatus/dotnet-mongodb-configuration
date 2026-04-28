using MongoDB.Driver;

namespace Solidatus.Extensions.Configuration.MongoDb.Internal;

internal static class MongoProvider
{
    private static IMongoCollection<ConfigDbEntry>? _collection;

    internal static IMongoCollection<ConfigDbEntry> GetCollection(MongoClientSettings clientSettings, string database,
        string collection)
    {
        return _collection ??= new MongoClient(clientSettings)
            .GetDatabase(database)
            .GetCollection<ConfigDbEntry>(collection);
    }

    internal static IMongoClient GetClient(MongoClientSettings clientSettings)
    {
        return new MongoClient(clientSettings);
    }

    internal static IMongoCollection<ConfigDbEntry> GetCollection()
    {
        return _collection ?? throw new("Attempted to access config entry collection before initialisation");
    }
}