namespace zoosim.core.Systems;

public interface IFatigueSystem : ISystem
{
    void Run(DateTime currentTime);
}
