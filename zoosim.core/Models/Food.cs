namespace zoosim.core.Models;

public class Food : IFood
{
    public Food(int value = 0)
    {
        HealingValue = value;
    }
    public virtual int HealingValue { get; private set; }

    public bool IsEaten => HealingValue == 0;

    public void Dispose()
    {
        HealingValue = 0;
    }
}
