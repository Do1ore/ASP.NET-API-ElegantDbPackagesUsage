using Application.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Data.EfCore;
using Infrastructure.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

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

            default: throw new ApplicationException($"Service {repositoryType.ToString()} not found");
        }
    }
}