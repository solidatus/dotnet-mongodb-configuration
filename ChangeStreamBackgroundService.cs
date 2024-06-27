using Microsoft.Extensions.Hosting;

namespace Solidatus.MongoDb.Configuration;

public class ChangeStreamBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}