using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Solidatus.MongoDb.Configuration;

public class ConfigDbEntry
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    public string Key { get; set; }
    
    public string? Value { get; set; }
}