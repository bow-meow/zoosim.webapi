using zoosim.core.Enums;

namespace zoosim.core.Models.Animals;

public interface IGiraffe : IAnimal { }
public class Giraffe : Animal, IGiraffe
{
    public override bool IsAlive => Health >= 50f;

    public override AnimalType AnimalType => AnimalType.Giraffe;
}
