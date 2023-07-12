using Infrastructure.Abstractions;
using Infrastructure.Enums;

namespace Application.Contracts;

public interface IRepositoryFactory
{
    Task<IDatabaseRepository> CreateRepository(RepositoryType repositoryType);
}