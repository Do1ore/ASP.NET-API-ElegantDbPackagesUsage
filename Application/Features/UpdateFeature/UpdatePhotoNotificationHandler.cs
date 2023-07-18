using Infrastructure.Abstractions;

namespace Application.Features.UpdateFeature;

public record UpdatePhotoNotificationHandler : INotificationHandler<UpdatePhotoNotification>
{
    private readonly IRedisService _redisService;

    public UpdatePhotoNotificationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Handle(UpdatePhotoNotification notification, CancellationToken cancellationToken)
    {
        var result = await _redisService.SetValue(notification.Photo.Id, notification.Photo);
        if(result.IsSuccess)
        {

        }
    }
}
