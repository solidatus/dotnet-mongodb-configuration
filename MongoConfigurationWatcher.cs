using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Solidatus.Extensions.Configuration.MongoDb.Internal;

namespace Solidatus.Extensions.Configuration.MongoDb;

public class MongoConfigurationWatcher : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cursor = await MongoProvider.GetCollection().WatchAsync(new ChangeStreamOptions(), stoppingToken);
        
        await cursor.ForEachAsync(_ =>
        {
            MongoConfigurationProvider.Get().Load();
        }, cancellationToken: stoppingToken);
    }
}