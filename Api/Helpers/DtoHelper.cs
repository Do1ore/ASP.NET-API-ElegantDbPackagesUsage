using Api.DTOs;

namespace Api.Helpers;

public static class DtoHelper
{
    public static Guid CheckOrGenerateEntityKey(ref PhotoDto photo)
    {
        if (photo.Id == Guid.Empty || !Guid.TryParse(photo.Id.ToString(), out _))
        {
            return photo.Id = Guid.NewGuid();
        }

        return photo.Id;
    }
}