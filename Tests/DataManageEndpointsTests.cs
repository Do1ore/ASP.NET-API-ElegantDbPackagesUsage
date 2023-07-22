using Api.EndpointsDefinitions;
using Application.Features.GetAllFeature;
using Application.Features.GetByIdFeature;

namespace Tests;

public class DataManageEndpointsTests
{
    [Fact]
    public async Task GetAllPhotos_Should_Return_All_Photos()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var repositoryName = "adonet";
        var token = CancellationToken.None;

        var expectedPhotos = new List<Photo> { new Photo(), new Photo() };
        mediator.Send(Arg.Any<GetAllPhotosRequest>(), token)
            .Returns(new Result<List<Photo>>(expectedPhotos));

        var endpointsDefinition = new DataManageEndpointsDefinition();

        // Act
        var result =
            (Ok<List<Photo>>)await endpointsDefinition.GetAllPhotos(mediator, repositoryName, token);

        // Assert
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task GetById_Should_Return_Photo()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var repositoryName = "adonet";
        var token = CancellationToken.None;
        var guid = Guid.NewGuid();

        var expectedResult = new Photo();
        mediator.Send(Arg.Any<GetByIdRequest>(), token).Returns(new Result<Photo>(expectedResult));

        var endpointsDefinition = new DataManageEndpointsDefinition();

        // Act
        var result = (Ok<Photo>)await endpointsDefinition.GetPhotoById(mediator, guid, repositoryName, token);

        // Assert
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(result.Value, expectedResult);
    }
}