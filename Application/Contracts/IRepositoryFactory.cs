using Infrastructure.Abstractions;
using Infrastructure.Enums;

namespace Application.Contracts
{
    /// <summary>
    /// Represents a factory for creating database repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a database repository based on the specified repository type.
        /// </summary>
        /// <param name="repositoryType">The repository type.</param>
        /// <returns>The created <see cref="IDatabaseRepository"/>.</returns>
        Task<IDatabaseRepository> CreateRepository(RepositoryType repositoryType);
    }
}