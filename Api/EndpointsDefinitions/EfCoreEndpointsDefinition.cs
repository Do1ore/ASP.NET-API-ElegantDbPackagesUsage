using Api.Abstractions;
using Application.Features.EfCoreFeatures;
using Domain.Entities;
using MediatR;

namespace Api.EndpointsDefinitions;

public class EfCoreEndpointsDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var photos = app.MapGroup("/photos");

        photos.MapGet("/", GetAllPhotos);

        photos.MapGet("/{id}", GetPhotoById);

        photos.MapPost("/", CreatePhoto);

        photos.MapPut("/{id}", UpdatePhoto);

        photos.MapDelete("/{id}", DeletePhoto);
    }


    private Task<IResult> GetAllPhotos(IMediator mediator, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private Task<IResult> UpdatePhoto(IMediator mediator, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private async Task<IResult> CreatePhoto(IMediator mediator, Photo photo, CancellationToken token)
    {
        var result = await mediator.Send(new AddPhotoRequest(photo), token);
        return TypedResults.Ok(photo);
    }

    private Task<IResult> GetPhotoById(IMediator mediator, CancellationToken token)
    {
        throw new NotImplementedException();
    }


    private async Task<IResult> DeletePhoto(IMediator mediator, string id)
    {
        var result = await mediator.Send(new AddPhotoRequest(new Photo()));
        return TypedResults.Ok();
    }
}