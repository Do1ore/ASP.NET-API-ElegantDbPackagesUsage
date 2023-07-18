using Domain.Entities;
using LanguageExt.Common;

namespace Infrastructure.Abstractions;

public interface IRedisService
{
    Task<Result<T>> GetById<T>(Guid id, CancellationToken cancellationToken);
    Task<Result<T>> Create<T>(Guid id, T photo, CancellationToken cancellationToken);
    Task<Result<T>> Update<T>(Guid id, T photo, CancellationToken cancellationToken);
    Task<Result<object>> Delete(Guid id, CancellationToken cancellationToken);
}