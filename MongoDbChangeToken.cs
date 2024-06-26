using Microsoft.Extensions.Primitives;
using MongoDB.Driver;

namespace MongodbConfiguration;

public class MongoDbChangeToken<T>(IChangeStreamCursor<T> cursor) : IChangeToken
{
    private IChangeStreamCursor<T> _cursor = cursor;

    public IDisposable RegisterChangeCallback(Action<object?> callback, object? state)
    {
        throw new NotImplementedException();
    }

    public bool HasChanged { get; } = false;
    public bool ActiveChangeCallbacks => true;
}