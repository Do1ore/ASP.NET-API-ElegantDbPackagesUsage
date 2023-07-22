using Api.Abstractions;
using Api.DTOs;
using Api.Extensions;
using Api.Helpers;
using Application.Features.AddFeature;
using Application.Features.DeleteFeature;
using Application.Features.GetAllFeature;
using Application.Features.GetByIdFeature;
using Application.Features.UpdateFeature;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Serilog;

namespace Api.EndpointsDefinitions;

public class DataManageEndpointsDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var photos = app.MapGroup("api/v1/photos");

        photos.MapGet("/", GetAllPhotos);

        photos.MapGet("/{id}", GetPhotoById);

        photos.MapPost("/", CreatePhoto);

        photos.MapPut("/", UpdatePhoto);

        photos.MapDelete("/{id}", DeletePhoto);
    }


    public async Task<IResult> GetAllPhotos(IMediator mediator, string repositoryName, CancellationToken token)
    {
        var result = await mediator.Send(new GetAllPhotosRequest(repositoryName.ToRepositoryType()), token);
        return result.ToOkResult(values => values);
    }

    public async Task<IResult> UpdatePhoto(IMediator mediator, string repositoryName, PhotoDto photoDto,
        CancellationToken token)
    {
        var result = await mediator.Send(new UpdatePhotoRequest(photoDto, repositoryName.ToRepositoryType()), token);
        return result.ToOkResult(values => values);
    }

    public async Task<IResult> CreatePhoto(IMediator mediator, string repositoryName, PhotoDto photoDto,
        CancellationToken token)
    {
        DtoHelper.CheckOrGenerateEntityKey(ref photoDto);

        var result = await mediator.Send(new AddPhotoRequest(photoDto, repositoryName.ToRepositoryType()), token);
        return result.ToOkResult(values => values);
    }

    public async Task<IResult> GetPhotoById(IMediator mediator, Guid id, string repositoryName,
        CancellationToken token)
    {
        var result = await mediator.Send(new GetByIdRequest(id, repositoryName.ToRepositoryType()), token);

        return result.ToOkResult(value => value);
    }


    public async Task<IResult> DeletePhoto(IMediator mediator, string repositoryName, Guid id)
    {
        var result = await mediator.Send(new DeletePhotoRequest(id, repositoryName.ToRepositoryType()));

        return result.ToOkResult(value => value);
    }
}