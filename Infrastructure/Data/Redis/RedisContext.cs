using Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Data.Redis;

public class RedisContext : IRedisContext
{
    private readonly string _connectionString;

    public RedisContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RedisConnection") ??
                            throw new ApplicationException("Connection string for redis not found");
    }

    public ConnectionMultiplexer GetConnection()
    {
        ConnectionMultiplexer connect = ConnectionMultiplexer.Connect(_connectionString);
        return connect;
    }
}