
using System.Collections.Concurrent;
using zoosim.core.Engines;
using zoosim.core.Enums;
using zoosim.core.Repositories;
using zoosim.webapi.Dtos;
using zoosim.webapi.Mappers;

namespace zoosim.webapi.Services;

public class ZooService : IZooService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAnimalRepository _animalRepository;
    private readonly ConcurrentDictionary<Guid, IZooEngine> _sessions = new ConcurrentDictionary<Guid, IZooEngine>();
    public ZooService(IServiceProvider serviceProvider,
        IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
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

    public IEnumerable<AnimalDto> GetAllAnimals(Guid id) 
    {
        var animals = GetOrCreateEngine(id).AllAnimals;

        var list = new List<AnimalDto>();

        foreach(var type in Enum.GetValues<AnimalType>())
        {
            var dtos = _animalRepository.GetAnimalsByType(type);
            for(int i = 0; i < dtos.Length; i++)
            {
                var dto = dtos[i];
                AnimalMapper.MapToDto(animals[i], dto);
                list.Add(dto);
            }
        }
        return list;
    }
    public void ReviveAnimals(Guid id) => GetOrCreateEngine(id).ReviveAnimals();
    public void CycleTimeBy1Hour(Guid id) => GetOrCreateEngine(id).CycleTime(TimeSpan.FromHours(1));
    public void FeedAnimals(Guid id) => GetOrCreateEngine(id).FeedAnimals();
}