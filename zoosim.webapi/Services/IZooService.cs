using zoosim.core.Models;

namespace zoosim.webapi.Services;

public interface IZooService
{
    IEnumerable<IAnimal> GetAllAnimals(Guid tabId);
    void ReviveAnimals(Guid tabId);
    void CycleTimeBy1Hour(Guid tabId);
    void FeedAnimals(Guid tabId);
}
