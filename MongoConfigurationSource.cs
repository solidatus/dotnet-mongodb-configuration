using Microsoft.Extensions.Configuration;
using MongodbConfiguration.Options;

namespace MongodbConfiguration;

public sealed class MongoConfigurationSource(MongoDbConfigurationOptions options) : IConfigurationSource
{
    private readonly MongoDbConfigurationOptions _options = options;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new MongoConfigurationProvider();
    }
}