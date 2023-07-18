using Infrastructure.Abstractions;

namespace Application.Features.AddFeature;

public class AddPhotoNotificationHandler : INotificationHandler<AddPhotoNotification>
{
    private readonly IRedisService _redisService;

    public AddPhotoNotificationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Handle(AddPhotoNotification notification, CancellationToken cancellationToken)
    {
        var result = await _redisService.SetValue(notification.Photo.Id, notification.Photo);
        if (result.IsFaulted)
        {
            //log
        }
    }
}