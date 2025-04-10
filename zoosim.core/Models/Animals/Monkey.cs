using zoosim.core.Enums;

namespace zoosim.core.Models.Animals;

public interface IMonkey : IAnimal { }
public class Monkey : Animal, IMonkey
{
    public override bool IsAlive => Health >= 30.0f;

    public override AnimalType AnimalType => AnimalType.Monkey;
}
