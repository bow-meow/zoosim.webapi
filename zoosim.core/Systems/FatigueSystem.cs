using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Models.Animals;
using zoosim.core.Utils;

namespace zoosim.core.Systems;

public class FatigueSystem : System, ISystem
{
    private readonly Dictionary<IAnimal, int> _hoursWithoutMoving = [];
    private readonly IDice _dice;
    public FatigueSystem(IDice dice,
        IAnimalManager animalManager)
        : base(animalManager)
    {
        _dice = dice;
    }

    protected override void Execute(IAnimal animal)
    {
        if (!animal.IsAlive)
            return;

        switch (animal)
        {
            case IElephant elephant:
                ElephantFatigue(elephant);
                break;
            default:
                Fatigue(animal);
                break;
        }
    }

    private void ElephantFatigue(IElephant elephant)
    {
        if (!elephant.CanMove)
        {
            if (!_hoursWithoutMoving.TryGetValue(elephant, out var hours))
                hours = 0;

            _hoursWithoutMoving[elephant] = ++hours;

            if (hours > 1)
                elephant.Kill();
            else
                Fatigue(elephant);
        }
        else
        {
            _hoursWithoutMoving[elephant] = 0;
            Fatigue(elephant);
        }
    }

    private void Fatigue(IAnimal animal)
    {
        var rng_amount = _dice.RollForFatigue();
        var dmg = animal.Health * (rng_amount / 100.0);
        animal.TakeDamage((int)dmg);
    }
}
