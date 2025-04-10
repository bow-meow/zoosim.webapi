using zoosim.core.Enums;
using zoosim.core.Models;

namespace zoosim.core.Engines;

public interface IZooEngine
{
    void FeedAnimals();
    void ReviveAnimals();
    void CycleTime(TimeSpan time);
    IAnimal[] GetAnimalsByType(AnimalType type);
    IAnimal[] AllAnimals { get; }
    bool AreAnimalsHungry { get; }
}
