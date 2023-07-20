using Infrastructure.Abstractions;

namespace Application.Features.GetByIdFeature;

public class GetByIdNotificationHandler : INotificationHandler<GetByIdNotification>
{
    private readonly IRedisService _redisService;

    public GetByIdNotificationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Handle(GetByIdNotification notification, CancellationToken cancellationToken)
    {
        var result = await _redisService.SetValue(notification.Photo.Id, notification.Photo);
        if (result.IsSuccess)
        {
        }

    }
}
