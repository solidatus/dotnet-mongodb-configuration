using MongoDB.Driver;
using Solidatus.Extensions.Configuration.MongoDb.Internal;

namespace Solidatus.Extensions.Configuration.MongoDb;

public static class MongoConfigurationManager
{
    /// <summary>
    /// Stores the given value in the configuration collection, overwriting an existing one if it exists
    /// </summary>
    /// <param name="key">Configuration key in the .Net format</param>
    /// <param name="value">Value to be stored</param>
    public static async Task SetValue(string key, string value)
    {
        var filter = Builders<ConfigDbEntry>.Filter.Eq(e => e.Key, key);
        
        var entry = new ConfigDbEntry
        {
            Key = key,
            Value = value
        };

        var replaceOptions = new ReplaceOptions
        {
            IsUpsert = true
        };

        await CollectionProvider.GetCollection().ReplaceOneAsync(filter, entry, replaceOptions);
    }

    /// <summary>
    /// Attempts to remove the provided configuration entry from the mongo collection
    /// </summary>
    /// <param name="key">Configuration key in the .Net format to remove</param>
    /// <returns>True if the key existed and was removed, false otherwise</returns>
    public static async Task<bool> TryRemoveValue(string key)
    {
        var filter = Builders<ConfigDbEntry>.Filter.Eq(e => e.Key, key);

        var deleteResult = await CollectionProvider.GetCollection().DeleteOneAsync(filter);

        return deleteResult.DeletedCount == 1;
    }
}