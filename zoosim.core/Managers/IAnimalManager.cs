using zoosim.core.Enums;
using zoosim.core.Models;

namespace zoosim.core.Managers;

public interface IAnimalManager
{
    IAnimal[] GetAnimalByType(AnimalType type);
    IEnumerable<IAnimal> AllAnimals { get; }
}
