namespace zoosim.core.Models;

public interface IFood : IDisposable
{
    int HealingValue { get; }
    bool IsEaten { get; }
}
