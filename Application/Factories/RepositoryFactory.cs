using Application.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Data.EfCore;
using Infrastructure.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factories
{
    /// <summary>
    /// Factory for creating database repositories.
    ///Implementation of <see cref="IRepositoryFactory"/>
    ///<seealso cref="RepositoryType"/>
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates a database repository based on the specified repository type.
        /// </summary>
        /// <param name="repositoryType">The repository type.</param>
        /// <returns>The created <see cref="IDatabaseRepository"/>.</returns>
        public Task<IDatabaseRepository> CreateRepository(RepositoryType repositoryType)
        {
            switch (repositoryType)
            {
                case RepositoryType.AdoNet:
                    var configuration = _serviceProvider.GetService<IConfiguration>();

                    if (configuration is null)
                    {
                        throw new ApplicationException($"Service {nameof(IConfiguration)} not found");
                    }

                    var adoNetRepository = new AdoNetRepository(configuration);

                    return Task.FromResult<IDatabaseRepository>(adoNetRepository);

                case RepositoryType.EfCore:
                    var context = _serviceProvider.GetService<EfCorePhotosContext>();

                    if (context is null)
                    {
                        throw new ApplicationException($"Service {nameof(EfCorePhotosContext)} not found");
                    }

                    var efCoreRepository = new EfCoreRepository(context);

                    return Task.FromResult<IDatabaseRepository>(efCoreRepository);

                case RepositoryType.Dapper:
                    var dbContext = _serviceProvider.GetService<IDbContext>();

                    if (dbContext is null)
                    {
                        throw new ApplicationException($"Service {nameof(IDbContext)} not found");
                    }

                    var dapperRepository = new DapperRepository(dbContext);

                    return Task.FromResult<IDatabaseRepository>(dapperRepository);

                default:
                    throw new ApplicationException($"Service {repositoryType.ToString()} not found");
            }
        }
    }
}