using System.ComponentModel;
using zoosim.core.Enums;

namespace zoosim.core.Models;

public interface IAnimal
{
    float Health { get; }
    void Eat(IFood food);
    bool CanMove { get; }
    AnimalType AnimalType { get; }
    bool IsAlive { get; }
    void Revive();
    bool IsHungry { get; }
    void TakeDamage(int amount);
    void Kill();
}
