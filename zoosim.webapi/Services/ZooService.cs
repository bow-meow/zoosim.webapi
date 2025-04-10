
using System.Collections.Concurrent;
using zoosim.core.Engines;
using zoosim.core.Models;
namespace zoosim.webapi.Services;

public class ZooService : IZooService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<Guid, IZooEngine> _sessions = new ConcurrentDictionary<Guid, IZooEngine>();
    public ZooService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private IZooEngine GetOrCreateEngine(Guid id) =>
        _sessions.GetOrAdd(id, id => 
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetService<IZooEngine>();
            }
        });

    public IEnumerable<IAnimal> GetAllAnimals(Guid id) => GetOrCreateEngine(id).AllAnimals;
    public void ReviveAnimals(Guid id) => GetOrCreateEngine(id).ReviveAnimals();
    public void CycleTimeBy1Hour(Guid id) => GetOrCreateEngine(id).CycleTime(TimeSpan.FromHours(1));
    public void FeedAnimals(Guid id) => GetOrCreateEngine(id).FeedAnimals();
}