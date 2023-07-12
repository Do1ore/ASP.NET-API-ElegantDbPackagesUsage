using Application.Factories;
using Infrastructure.Data.EfCore;
using Infrastructure.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Microsoft.Extensions.Configuration;

namespace Tests;

public class RepositoryFactoryTests
{
    [Fact]
    public async Task CreateRepository_WithAdoNetType_ReturnsAdoNetRepository()
    {
        // Arrange
        var serviceProvider = Substitute.For<IServiceProvider>();
        var configuration = Substitute.For<IConfiguration>();
        serviceProvider.GetService<IConfiguration>().Returns(configuration);
        var repositoryFactory = new RepositoryFactory(serviceProvider);

        // Act
        var result = await repositoryFactory.CreateRepository(RepositoryType.AdoNet);

        // Assert
        Assert.IsType<AdoNetRepository>(result);
    }

    [Fact]
    public async Task CreateRepository_WithEfCoreType_ReturnsEfCoreRepository()
    {
        // Arrange
        var serviceProvider = Substitute.For<IServiceProvider>();
        var context = Substitute.For<EfCorePhotosContext>();
        serviceProvider.GetService<EfCorePhotosContext>().Returns(context);
        var repositoryFactory = new RepositoryFactory(serviceProvider);

        // Act
        var result = await repositoryFactory.CreateRepository(RepositoryType.EfCore);

        // Assert
        Assert.IsType<EfCoreRepository>(result);
    }
   
}