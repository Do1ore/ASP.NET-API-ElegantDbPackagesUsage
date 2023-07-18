using Domain.Entities;
using LanguageExt.Common;

namespace Infrastructure.Abstractions;

public interface IRedisService
{
    Task<Result<T>> GetById<T>(Guid id);
    Task<Result<T>> SetValue<T>(Guid id, T photo);
    Task<Result<object>> Delete(Guid id);
}