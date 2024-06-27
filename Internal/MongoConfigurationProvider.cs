using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MongoDB.Driver;

namespace Solidatus.Extensions.Configuration.MongoDb.Internal;

internal sealed class MongoConfigurationProvider(IMongoCollection<ConfigDbEntry> collection) : IConfigurationProvider
{
    private readonly ConfigurationReloadToken _reloadToken = new();
    
    public bool TryGet(string key, out string? value)
    {
        var entry = collection.Find(Builders<ConfigDbEntry>.Filter.Eq(e => e.Key, key)).SingleOrDefault();

        value = entry?.Value ?? null;

        return entry is not null;
    }

    public void Set(string key, string? value)
    {
        var updateOptions = new ReplaceOptions
        {
            IsUpsert = true
        };

        var entry = new ConfigDbEntry
        {
            Key = key,
            Value = value
        };

        collection.ReplaceOne(
            Builders<ConfigDbEntry>.Filter.Eq(e => e.Key, key),
            entry,
            updateOptions);
    }

    public IChangeToken GetReloadToken()
    {
        return this._reloadToken;
    }
    
    public void Load()
    {
        collection.Indexes.CreateOne(new CreateIndexModel<ConfigDbEntry>(Builders<ConfigDbEntry>.IndexKeys.Hashed(e => e.Key)));
    }
    
    public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string? parentPath)
    {
        var filter = Builders<ConfigDbEntry>.Filter.Empty;

        if (parentPath is not null)
        {
            filter = Builders<ConfigDbEntry>.Filter.Regex(entry => entry.Key,
                new($"^{parentPath}"));
        }

        var childKeys = collection
            .Find(filter)
            .ToList()
            .Select(entry => ExtractNextKeySection(entry.Key, parentPath?.Length ?? 0))
            .ToList();

        childKeys.AddRange(earlierKeys);
        childKeys.Sort();

        return childKeys;
    }

    private static string ExtractNextKeySection(string key, int prefixLength)
    {
        var nextSectionBreak = key.IndexOf(':', prefixLength);

        return nextSectionBreak is -1
            ? key.Substring(prefixLength)
            : key.Substring(prefixLength, nextSectionBreak - prefixLength);
    }
}