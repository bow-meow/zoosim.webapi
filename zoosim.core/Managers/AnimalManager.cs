using zoosim.core.Enums;
using zoosim.core.Factories;
using zoosim.core.Models;
using zoosim.core.Repositories;

namespace zoosim.core.Managers;

internal class AnimalManager : IAnimalManager
{
    private readonly IDictionary<AnimalType, List<IAnimal>> _animalDictionary = new Dictionary<AnimalType, List<IAnimal>>();
    private readonly List<IAnimal> _allAnimals = [];
    public IEnumerable<IAnimal> AllAnimals => _allAnimals;

    public AnimalManager(IAnimalRepository repository, IAnimalFactory factory)
    {
        foreach(var type in Enum.GetValues<AnimalType>())
        {
            var animals = repository.GetAnimalsByType(type);

            foreach(var animal in animals)
                AddAnimal(factory.CreateAnimal(animal.Type));
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
        return _animalDictionary.TryGetValue(type, out List<IAnimal> value) ? [.. value] : [];
    }
}
