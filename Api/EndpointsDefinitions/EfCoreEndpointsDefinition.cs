using Api.Abstractions;
using Api.DTOs;
using Application.Features.EfCoreFeatures.AddFeature;
using Application.Features.EfCoreFeatures.DeleteFeature;
using Application.Features.EfCoreFeatures.GetAllFeature;
using Application.Features.EfCoreFeatures.GetByIdFeature;
using Application.Features.EfCoreFeatures.UpdateFeature;
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

        photos.MapPut("/", UpdatePhoto);

        photos.MapDelete("/{id}", DeletePhoto);
    }


    private async Task<IResult> GetAllPhotos(IMediator mediator, CancellationToken token)
    {
        var result = await mediator.Send(new GetAllPhotosRequest(), token);
        return result.Match<IResult>(TypedResults.Ok,
            exception => TypedResults.BadRequest(
                new ErrorModel(StatusCodes.Status400BadRequest, exception.Message)));
    }

    private async Task<IResult> UpdatePhoto(IMediator mediator, PhotoDto photoDto, CancellationToken token)
    {
        var result = await mediator.Send(new UpdatePhotoRequest(photoDto), token);
        return result.Match<IResult>(Succ: TypedResults.Ok<Photo>,
            exception => TypedResults.BadRequest(new ErrorModel(StatusCodes.Status400BadRequest, exception.Message)));
    }

    private async Task<IResult> CreatePhoto(IMediator mediator, PhotoDto photo, CancellationToken token)
    {
        var result = await mediator.Send(new AddPhotoRequest(photo), token);
        return result.Match<IResult>(TypedResults.Ok, exception =>
            TypedResults.BadRequest(new ErrorModel(
                StatusCodes.Status400BadRequest,
                exception.Message)));
    }

    private async Task<IResult> GetPhotoById(IMediator mediator, Guid id, CancellationToken token)
    {
        var result = await mediator.Send(new GetByIdRequest(id), token);

        return result.Match<IResult>(TypedResults.Ok,
            exception => TypedResults.BadRequest(
                new ErrorModel(StatusCodes.Status400BadRequest, exception.Message)));
    }


    private async Task<IResult> DeletePhoto(IMediator mediator, Guid id)
    {
        var result = await mediator.Send(new DeletePhotoRequest(id));

        return result.Match<IResult>(value => TypedResults.Ok("Rows deleted: " + value),
            exception => TypedResults.BadRequest(new ErrorModel(
                StatusCodes.Status500InternalServerError,
                exception.Message)));
    }
}