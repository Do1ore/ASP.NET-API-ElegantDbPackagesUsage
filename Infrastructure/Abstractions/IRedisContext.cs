using StackExchange.Redis;

namespace Infrastructure.Abstractions;

public interface IRedisContext
{
    ConnectionMultiplexer GetConnection();
}