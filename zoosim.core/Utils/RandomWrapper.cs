namespace zoosim.core.Utils;

public interface IRandomWrapper
{
    int Next(int minValue, int maxValue);
}
internal class RandomWrapper : IRandomWrapper
{
    public int Next(int minValue, int maxValue) => Random.Shared.Next(minValue, maxValue);
}
