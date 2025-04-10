using zoosim.core.Enums;
using zoosim.core.Models;

namespace zoosim.core.Factories;

public interface IAnimalFactory
{
    IAnimal CreateAnimal(AnimalType animalType);
}
