using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Solidatus.Extensions.Configuration.MongoDb.Internal;

internal sealed class MongoConfigurationProvider : ConfigurationProvider
{
    private static MongoConfigurationProvider? _instance;
    
    private readonly IMongoCollection<ConfigDbEntry> _collection;
    
    private MongoConfigurationProvider(IMongoCollection<ConfigDbEntry> collection)
    {
        this._collection = collection;
        
        collection.Indexes.CreateOne(
            new CreateIndexModel<ConfigDbEntry>(
                Builders<ConfigDbEntry>.IndexKeys.Descending(e => e.Key),
                new CreateIndexOptions { Unique = true }
            )
        );
    }

    public static MongoConfigurationProvider Create(IMongoCollection<ConfigDbEntry> collection)
    {
        if (_instance is not null) return _instance;
        
        if (collection is null)
        {
            throw new ArgumentException("Collection is required when provider first constructed",
                nameof(collection));
        }

        return _instance = new MongoConfigurationProvider(collection);
    }

    public static MongoConfigurationProvider Get()
    {
        return _instance ?? throw new InvalidOperationException("MongoConfigurationProvider is not initialized");
    }
    
    /// <summary>
    /// Called when the configuration source is added, this ensures that the key field can be quickly queried
    /// </summary>
    public override void Load()
    {
        this.Data = this._collection
            .Find(Builders<ConfigDbEntry>.Filter.Empty)
            .ToList()
            .ToDictionary(x => x.Key, x => x.Value);
    }
}