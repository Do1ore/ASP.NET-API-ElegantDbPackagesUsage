using System.Text.Json;
using Infrastructure.Abstractions;
using LanguageExt.Common;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase _cacheDb;
    private readonly int _defaultExpirationTime;

    public RedisService(IRedisContext redis, IConfiguration configuration)
    {
        _cacheDb = redis.GetConnection().GetDatabase();
        var a = redis.GetConnection().GetEndPoints();
        var expirationTime = configuration.GetSection("Redis")["DefaultExpirationTimeInMinutes"] ??
                             throw new Exception("Expiration time not found");
        _defaultExpirationTime = Convert.ToInt32(expirationTime);
    }

    public async Task<Result<T>> GetById<T>(Guid id)
    {
        var value = await _cacheDb.StringGetSetExpiryAsync(id.ToString(), TimeSpan.FromMinutes(_defaultExpirationTime));
        if (!value.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<T>(value);
        }
       
        return new Result<T>(new Exception("Cannot get value"));
    }

    public async Task<Result<T>> SetValue<T>(Guid id, T value)
    {
        var valueToAdd = JsonSerializer.Serialize(value);
        var isSucceed =
            await _cacheDb.StringSetAsync(id.ToString(), valueToAdd, TimeSpan.FromMinutes(_defaultExpirationTime));

        if (isSucceed)
        {
            return value;
        }

        return new Result<T>(new ApplicationException("Cannot add value to redis storage"));
    }

    public async Task<Result<object>> Delete(Guid id)
    {
        if (!await _cacheDb.KeyExistsAsync(id.ToString()))
        {
            return new Result<object>(new ArgumentException("Value with this key is not exists in redis storage"));
        }

        var isSuccessDeletedAsync = await _cacheDb.KeyDeleteAsync(id.ToString());

        if (isSuccessDeletedAsync)
        {
            return id;
        }

        return new Result<object>(
            new ApplicationException("Unknown error. Value cannot be deleted from redis storage."));
    }
}