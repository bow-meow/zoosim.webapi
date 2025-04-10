namespace zoosim.core.Utils;

public class Dice : IDice
{
    private readonly IRandomWrapper _randomWrapper;
    public Dice(IRandomWrapper randomWrapper)
    {
        _randomWrapper = randomWrapper;
    }

    public int Roll(int min, int max) => _randomWrapper.Next(min, max);

    public int RollForHeal()
    {
        return _randomWrapper.Next(10, 26);
    }

    public int RollForFatigue()
    {
        return _randomWrapper.Next(0, 21);
    }
}
