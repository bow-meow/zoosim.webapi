using zoosim.core.Configuration;
using zoosim.core.Enums;
using zoosim.core.Factories;
using zoosim.core.Models;

namespace zoosim.core.Managers;

internal class AnimalManager : IAnimalManager
{
    private IDictionary<AnimalType, List<IAnimal>> _animalDictionary = new Dictionary<AnimalType, List<IAnimal>>();
    private List<IAnimal> _allAnimals { get; } = [];
    public IEnumerable<IAnimal> AllAnimals => _allAnimals;

    public AnimalManager(ZooConfiguration config, IAnimalFactory factory)
    {
        foreach (var animalInfo in config.AnimalInfos)
        {
            for (int i = 0; i < animalInfo.NumOfAnimal; i++)
            {
                var animal = factory.CreateAnimal(animalInfo.AnimalType);

                AddAnimal(animal);
            }
        }
    }

    private void AddAnimal(IAnimal animal)
    {
        _allAnimals.Add(animal);

        if (!_animalDictionary.ContainsKey(animal.AnimalType))
        {
            _animalDictionary[animal.AnimalType] = [];
        }

        _animalDictionary[animal.AnimalType].Add(animal);
    }

    public IAnimal[] GetAnimalsByType(AnimalType type)
    {
        return _animalDictionary.ContainsKey(type) ? [.. _animalDictionary[type]] : [];
    }
}
