using Infrastructure.Abstractions;

namespace Application.Features.DeleteFeature;

public class DeletePhotoNotificationHandler : INotificationHandler<DeletePhotoNotification>
{
    private readonly IRedisService _redisService;

    public DeletePhotoNotificationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Handle(DeletePhotoNotification notification, CancellationToken cancellationToken)
    {
        var result = await _redisService.Delete(notification.Id);

        if(result.IsSuccess)
        {

        }
    }
}
