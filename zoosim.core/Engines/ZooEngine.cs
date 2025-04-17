using zoosim.core.Enums;
using zoosim.core.Factories;
using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Systems;

namespace zoosim.core.Engines;

public class ZooEngine : IZooEngine
{
    private readonly IFoodFactory _foodFactory;
    private readonly IAnimalManager _animalManager;
    private readonly IClock _clock;

    public ZooEngine(IAnimalManager animalManager,
        IFoodFactory foodFactory,
        IClock clock,
        IEnumerable<ISystem> systems)
    {
        _foodFactory = foodFactory;
        _animalManager = animalManager;
        _clock = clock;

        _clock.AddSystems(systems);
    }

    public void FeedAnimals()
    {
        foreach (var type in Enum.GetValues<AnimalType>())
        {
            var animals = _animalManager.GetAnimalsByType(type);
            var foods = _foodFactory.GetFood(animals.Length);

            for (int i = 0; i < animals.Length; i++)
                animals[i].Eat(foods[i]);
        }
    }

    public void ReviveAnimals()
    {
        foreach (var animal in _animalManager.AllAnimals)
            animal.Revive();
    }

    public bool AreAnimalsHungry => _animalManager.AllAnimals.Any(animal => animal.IsHungry);

    public IAnimal[] AllAnimals => [.. _animalManager.AllAnimals];

    public void CycleTime(TimeSpan time)
    {
        _clock.AddTime(time);
    }

    public IAnimal[] GetAnimalsByType(AnimalType type) => _animalManager.GetAnimalsByType(type);
}
