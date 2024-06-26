using MongoDB.Bson.Serialization.Attributes;

namespace MongodbConfiguration;

public class ConfigDbEntry
{
    [BsonId]
    public Guid Id { get; set; }
    
    public string Key { get; set; }
    
    public string? Value { get; set; }
}