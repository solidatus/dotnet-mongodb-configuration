using JetBrains.Annotations;

namespace MongodbConfiguration.Options;

[PublicAPI]
public class MongoDbConfigurationOptions
{
    public string Host { get; set; }
    
    public string? Database { get; set; }
    
    public string? UserName { get; set; }
    
    public string? Password { get; set; }
    
    public string? AuthenticationDatabase { get; set; }
    
    public string? ConfigurationCollection { get; set; }
}