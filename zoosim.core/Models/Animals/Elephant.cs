using zoosim.core.Enums;

namespace zoosim.core.Models.Animals;

public interface IElephant : IAnimal { }
public class Elephant : Animal, IElephant
{
    public override bool CanMove => Health >= 70f;

    public override bool IsAlive => Health > 0;

    public override AnimalType AnimalType => AnimalType.Elephant;
}
