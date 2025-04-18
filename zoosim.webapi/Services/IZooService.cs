using zoosim.core.Models;
using zoosim.webapi.Dtos;

namespace zoosim.webapi.Services;

public interface IZooService
{
    IEnumerable<AnimalDto> GetAllAnimals(Guid tabId);
    void ReviveAnimals(Guid tabId);
    void CycleTimeBy1Hour(Guid tabId);
    void FeedAnimals(Guid tabId);
}
