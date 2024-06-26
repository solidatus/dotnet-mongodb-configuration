using Microsoft.Extensions.Hosting;

namespace MongodbConfiguration;

public class ChangeStreamBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}